using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public static game_manager gameManager;

    private AppData lad;
    private AvatarData avatar_data;

    [SerializeField] public int char_points;
    public int challenge_points;
    public int multiplayer_points;
    public int total_points;

    public Text charpointsText;
    public Text challengepointsText;

    // store the ID of the weapon the player is currently using
    public int currentWeaponID = 0;

    public string gender;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        gender = "male";

        // Read in the character and challenge points and the currently used avatar ID from Firebase to update the avatar shop UI.
        var dataMgr = DataManager.getInstance();
        dataMgr.getGameData((result, msg) =>
        {
            char_points = result.charPoints;
            challenge_points = result.challengePoints;
            multiplayer_points = result.multiPlayerPoints;
            total_points = challenge_points + multiplayer_points;
            currentWeaponID = result.avatar.avatar_id;
            UpdateUI();
        }, true);

    }

    // Adds character points
    public void add_points(int points)
    {
        char_points += points;

        UpdateUI();
    }

    // Subtracts character points
    public void reduce_points(int points)
    {
        char_points -= points;
        UpdateUI();
    }

    // Returns true if user has sufficient character points to purchase a particular avatar
    public bool readPoints(int points)
    {
        if (points <= char_points)
        {
            return true;
        }
        return false;
    }

    // Returns the current amount of character points
    public int get_points()
    {
        return char_points;
    }

    // Set the amount of character points
    public void set_char_points(int points)
    {
        char_points = points;
        UpdateUI();
    }

    // Updates the UI with the current amount of character points and challenge points.
    void UpdateUI()
    {
        charpointsText.text = "        " + char_points.ToString("D") + " Points";
        challengepointsText.text = "        " + total_points.ToString("D") + " Points";
    }
}
