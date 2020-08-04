using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class item_holder : MonoBehaviour
{
    // Unique ID assigned to each avatar
    public int item_id;
    // Name of the avatar
    public Text item_name;
    // Price of the avatar character
    public Text item_price;
    // Sprite of the avatar
    public Image item_image;
    // The buy button object associated with each item holder.
    public GameObject buy_button;


}
