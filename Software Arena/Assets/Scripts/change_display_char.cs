using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class change_display_char : MonoBehaviour
{
    public int currentWeaponID;
    public List<weapon> shoplist;
    Image imgComponent;

    // Start is called before the first frame update
    void Start()
    {
        shoplist = new List<weapon>();
        imgComponent = GetComponent<Image>();

        get_curr_char();
    }

    // Reads in the ID of the avatar character that is currently being used by the user from Firebase and 
    // updates the UI to display the corresponding avatar character.
    private void get_curr_char()
    {
        var dataMgr = DataManager.getInstance();
        dataMgr.getGameData((result, msg) =>
        {
            if (result == null)
            {
                print("Get avatar shop state from database failed!");
            }
            else
            {
                // store the currently used avatar ID
                currentWeaponID = result.avatar.avatar_id;
                shoplist = result.avatar.shoplist;

                UpdateChar(currentWeaponID);
            }
        });

        
    }

    // Updates the UI Image object to display the avatar sprite.
    public void UpdateChar(int current_weaponid)
    {
        var tempColor = imgComponent.color;

        for (int i = 0; i < shoplist.Count; i++)
        {
            // If this is the weapon the player is currently using, then we will display it in the middle of the avatar shop
            if (shoplist[i].weapon_id == current_weaponid)
            {
                // load in the appropriate sprite depending on the currently used avatar ID.
                imgComponent.sprite = Resources.Load<Sprite>("Sprites/" + "male" + "/" + "hero" + shoplist[i].weapon_id + "/1");
                tempColor.a = 255f; // set the alpha to be 255.
                imgComponent.color = tempColor;
            }
        }
    }
}
