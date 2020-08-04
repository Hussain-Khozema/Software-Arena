using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_system : MonoBehaviour
{
    public static shop_system shop;
    public List<weapon> weapon_list = new List<weapon>();

    public List<GameObject> item_holder_list = new List<GameObject>();

    public List<GameObject> buy_button_list = new List<GameObject>();

    public GameObject item_holder_prefab;

    public Transform grid;

    public GameObject warningImg;
    private bool warningActive = false;

    // Start is called before the first frame update
    void Start()
    {
        warningImg.SetActive(false);
        shop = this;

        var dataMgr = DataManager.getInstance();
        dataMgr.getGameData((result, msg) =>
        {
            // Read in the avatar shop state from Firebase.
            weapon_list = result.avatar.shoplist;
            game_manager.gameManager.currentWeaponID = result.avatar.avatar_id;
            game_manager.gameManager.char_points = result.charPoints;

            fillList();

            // Update the text of each item holder according to whether the avatar has been bought or not.
            for (int i = 0; i < weapon_list.Count; i++)
            {
                //update item holder
                shop.update_sprite(weapon_list[i].weapon_id);

                // update the buttons
                shop.update_buy_button(game_manager.gameManager.currentWeaponID);
            }
        }, true);
    }

    // Used to populate the avatar shop list with all the avatars and their prices, etc.
    void fillList()
    {
        for (int i = 0; i < weapon_list.Count; i++)
        {
            GameObject holder = Instantiate(item_holder_prefab, grid, false);
            item_holder holder_script = holder.GetComponent<item_holder>();

            holder_script.item_name.text = weapon_list[i].weapon_name;
            holder_script.item_price.text = weapon_list[i].weapon_price.ToString();
            holder_script.item_id = weapon_list[i].weapon_id;

            // can check here if the char is male or female then load in the appropriate image
            holder_script.item_image.sprite = Resources.Load<Sprite>("Sprites/" + game_manager.gameManager.gender + "/" + "hero" + weapon_list[i].weapon_id + "/1");

            //BUY button
            holder_script.buy_button.GetComponent<buy_button>().weapon_id = weapon_list[i].weapon_id;

            item_holder_list.Add(holder);
            buy_button_list.Add(holder_script.buy_button);
        }
    }

    // Changes the text in each item holder. If the avatar has been bought by the user, this method will
    // change the text from the price of the avatar to "SOLD".
    public void update_sprite(int weapon_id)
    {
        for (int i = 0; i < item_holder_list.Count; i++) {
            item_holder holder_script = item_holder_list[i].GetComponent<item_holder>();
            if (holder_script.item_id == weapon_id) {
                for (int j = 0; j < weapon_list.Count; j++) {
                    if (weapon_list[j].weapon_id == weapon_id) {
                        // If the avatar has been bought, change the text to "SOLD".
                        if (weapon_list[j].bought) {
                            holder_script.item_price.text = "SOLD";
                        }
                    }

                }
            }
        }
    }

    // This method will update the text fields in each item holder depending on whether or not the user has purchased that avatar.
    public void update_buy_button(int current_weaponid)
    {

        for (int i = 0; i < buy_button_list.Count; i++)
        {
            buy_button buy_button_script = buy_button_list[i].GetComponent<buy_button>();
            for (int j = 0; j < weapon_list.Count; j++)
            {
                // If the avatar has been bought by the user and it is currently not being used by the user, we will display "USE" as the text.
                // This will indicate to the user that he/she may switch to using this avatar.
                if (weapon_list[j].weapon_id == buy_button_script.weapon_id && weapon_list[j].bought && weapon_list[j].weapon_id != current_weaponid)
                {
                    buy_button_script.button_text.text = "USE";
                }
                // If the avatar has been bought by the user and it is currently being used by the user, we will display "USING" as the text.
                // This will indicate to the user that he/she is currently using this particular avatar. 
                else if (weapon_list[j].weapon_id == buy_button_script.weapon_id && weapon_list[j].bought && weapon_list[j].weapon_id == current_weaponid)
                {
                    buy_button_script.button_text.text = "USING";
                    
                }

            }
        }
    }


    // Used to display the popup box indicating to the user that the user has insufficient 
    // character points to purchase a certain avatar character.
    public IEnumerator showWarning()
    {
        if (warningActive)
        {
            yield break;
        }

        warningActive = true;
        warningImg.SetActive(true);
        yield return new WaitForSeconds(1f);

        warningImg.SetActive(false);
        warningActive = false;
    }
}
