using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_main_char : MonoBehaviour
{
    Image imgComponent;

    void Start()
    {
        imgComponent = GetComponent<Image>();
    }

    // Updates the UI by displaying the currently used avatar sprite.
    void Update()
    {
        int current_weaponid = game_manager.gameManager.currentWeaponID;
        var tempColor = imgComponent.color;

        for (int i = 0; i < shop_system.shop.weapon_list.Count; i++)
        {
            // If this is the weapon the player is currently using, then we will display it in the middle of the avatar shop
            if (shop_system.shop.weapon_list[i].weapon_id == current_weaponid)
            {
                imgComponent.sprite = Resources.Load<Sprite>("Sprites/" + game_manager.gameManager.gender + "/" + "hero" + shop_system.shop.weapon_list[i].weapon_id + "/1");
                tempColor.a = 255f;
                imgComponent.color = tempColor;
                
            }

        } 
        
    }
}
