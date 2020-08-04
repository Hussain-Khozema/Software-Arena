using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class buy_button : MonoBehaviour
{
    public int weapon_id;
    public Text button_text;

    private AppData app_data;
    private GameProgress gp;
    private int challenge_pts;
    private int multi_pts;

    private static Action<bool, string> callback;

    // This method will be executed whenever the button to purchase an avatar is clicked on.
    public void buy_item()
    {
        // Read in the avatar shop state from Firebase.
        var dataMgr = DataManager.getInstance();
        dataMgr.getGameData((result, msg) =>
        {
            shop_system.shop.weapon_list = result.avatar.shoplist;
            game_manager.gameManager.currentWeaponID = result.avatar.avatar_id;
            game_manager.gameManager.char_points = result.charPoints;
            gp = result.progress;
            challenge_pts = result.challengePoints;
            multi_pts = result.multiPlayerPoints;

            if (weapon_id == 0)
            {
                Debug.Log("No weapon ID has been set!");
                return;
            }

            // Update the BUY button accordingly.
            for (int i = 0; i < shop_system.shop.weapon_list.Count; i++)
            {
                if (shop_system.shop.weapon_list[i].weapon_id == weapon_id)
                {
                    // if the avatar has not been bought
                    if (!shop_system.shop.weapon_list[i].bought)
                    {
                        // if the user has sufficient character points to buy the particular avatar
                        if (game_manager.gameManager.readPoints(shop_system.shop.weapon_list[i].weapon_price))
                        {
                            shop_system.shop.weapon_list[i].bought = true;
                            // Deduct the character points from the user
                            game_manager.gameManager.reduce_points(shop_system.shop.weapon_list[i].weapon_price);

                            // set avatar id
                            game_manager.gameManager.currentWeaponID = weapon_id;
                            // call update_buy_button method from shop_system.cs to update the text in the buy button accordingly.
                            update_buy_button(game_manager.gameManager.currentWeaponID);

                            // Save the updated avatar shop state to Firebase
                            result.avatar.shoplist = shop_system.shop.weapon_list;
                            result.avatar.avatar_id = game_manager.gameManager.currentWeaponID;
                            result.charPoints = game_manager.gameManager.char_points;
                            result.progress = gp;
                            result.challengePoints = challenge_pts;
                            result.multiPlayerPoints = multi_pts;

                            dataMgr.updateGameData(result, (saved_result, savemsg) =>
                            {
                                Debug.Log("Saved avatar shop state: " + saved_result + ", " + savemsg);
                            });
                        }
                        else
                        {
                            // Will display a popup dialog to indicate that the user has insufficient character points to purchase a particular avatar.
                            Debug.Log("Not enough money!");
                            StartCoroutine(shop_system.shop.showWarning());
                        }
                    }
                    else
                    {
                        // This block of code will execute when the user wants to switch to a different avatar that the user had previously purchased.
                        Debug.Log("Item has been bought!");
                        game_manager.gameManager.currentWeaponID = weapon_id;
                        update_buy_button(game_manager.gameManager.currentWeaponID);

                        result.avatar.shoplist = shop_system.shop.weapon_list;
                        result.avatar.avatar_id = game_manager.gameManager.currentWeaponID;
                        result.charPoints = game_manager.gameManager.char_points;
                        result.progress = gp;
                        result.challengePoints = challenge_pts;
                        result.multiPlayerPoints = multi_pts;
                        dataMgr.updateGameData(result, (saved_result, savemsg) =>
                        {
                            Debug.Log("Saved avatar shop state: " + saved_result + ", " + savemsg);
                        });
                        
                    }
                }

            }
            // Call the update_sprite() method from shop_system.cs 
            shop_system.shop.update_sprite(weapon_id);
        }, true);
        
    }

    void update_buy_button(int currentWeaponID)
    {
        shop_system.shop.update_buy_button(currentWeaponID);
    }
}
