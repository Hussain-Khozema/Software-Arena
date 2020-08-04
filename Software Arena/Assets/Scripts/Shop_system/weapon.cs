using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class weapon
{
    // Name of the avatar.
    public string weapon_name;
    // Unique ID assigned to an avatar.
    public int weapon_id;
    // Name of the avatar sprite file.
    public string weapon_img_name;
    // amount of character points needed to purchase this avatar.
    public int weapon_price;
    // if true, this means that the avatar has been bought
    public bool bought;

}
