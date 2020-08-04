using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   
    AppData lad;

    // Start is called before the first frame update
    void Start()
    {
        lad = new AppData();
        getData();

    }

    // This function will request for game progress of the player
    void getData()
    {
        DataManager fbMgr = DataManager.getInstance();

        fbMgr.getGameData((result, msg) =>
        {
            if (result == null)
            {
                print("Get question failed from database!");
            }
            else
            {
                lad = result;
                assignValues(lad);
                PlayerPrefs.SetString("overview", result.progress.curWorld + "-" + result.progress.curStage);
            }
        },true);

    }

    // This function will assign the game progress from the database to the following game objects
    // player name & character points
    void assignValues(AppData dal)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if(String.Equals("challenge_menu", sceneName)){
            Text userNameChallenge = GameObject.Find("username").GetComponent<Text>();
            userNameChallenge.text = "" + dal.name + "";

        }
        else
        {
            Text hh = GameObject.Find("charPoint").GetComponent<Text>();
            Text userNameChallenge = GameObject.Find("username").GetComponent<Text>();
            Text cmPoints = GameObject.Find("leaderPoints").GetComponent<Text>();
            hh.text = "" + dal.charPoints + "";
            userNameChallenge.text = "" + dal.name + "";
            cmPoints.text = "" + (dal.multiPlayerPoints + dal.challengePoints) + "";
        }
    }
}
