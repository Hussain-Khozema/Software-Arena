using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class saving_data : MonoBehaviour
{
    [System.Serializable]
    public class savedata
    {
        public List<weapon> shoplist = new List<weapon>();
        public int char_points;
        public int current_weapon_id;
    }

    // to store the weapon items and their info.
    public List<weapon> shoplist = new List<weapon>();

    public static saving_data save_data_obj;

    void Start()
    {
        // I need this so that the previously saved shop state will be loaded when player opens the avatar shop
        save_data_obj = this;
    }

    public void saving_to_firebase()
    {
        // we can save everything in the savedata class.
        savedata data = new savedata();
        // save the character points
        data.char_points = game_manager.gameManager.get_points();
        // save the weapon the player is currently using
        data.current_weapon_id = game_manager.gameManager.currentWeaponID;

        // add all weapons from the weapon shoplist
        for (int i = 0; i < shop_system.shop.weapon_list.Count; i++)
        {
            data.shoplist.Add(shop_system.shop.weapon_list[i]);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream_items = new FileStream("./shop_info.save", FileMode.Create);

        // serialize the data
        bf.Serialize(stream_items, data);
        stream_items.Close();

        print("Saved shop state!");
    }

    public void Saving()
    {
        // we can save everything in the savedata class.
        savedata data = new savedata();
        // save the character points
        data.char_points = game_manager.gameManager.get_points();
        // save the weapon the player is currently using
        data.current_weapon_id = game_manager.gameManager.currentWeaponID;

        // add all weapons from the weapon shoplist
        for (int i = 0; i < shop_system.shop.weapon_list.Count; i++)
        {
            data.shoplist.Add(shop_system.shop.weapon_list[i]);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream_items = new FileStream("./shop_info.save", FileMode.Create);

        // serialize the data
        bf.Serialize(stream_items, data);
        stream_items.Close();

        print("Saved shop state!");
        SceneManager.LoadScene(2);
    }

    public void Load()
    {
        if (File.Exists("./shop_info.save"))
        {
            print("Loading shop state!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream_items = new FileStream("./shop_info.save", FileMode.Open);

            savedata data = (savedata)bf.Deserialize(stream_items);
            game_manager.gameManager.set_char_points(data.char_points);
            game_manager.gameManager.currentWeaponID = data.current_weapon_id;

            stream_items.Close();


            //After loading the shop state into shoplist, we will for loop through shoplist and populate the shop accordingly
            for (int i = 0; i < data.shoplist.Count; i++)
            {
                // update the shop state with what we saved.
                shop_system.shop.weapon_list[i] = data.shoplist[i];

                //update item holder
                shop_system.shop.update_sprite(shop_system.shop.weapon_list[i].weapon_id);

                // update the buttons
                shop_system.shop.update_buy_button(game_manager.gameManager.currentWeaponID);
            }

        }
        else
        {
            print("No previously saved avatar shop state exists!");
        }
    }

}