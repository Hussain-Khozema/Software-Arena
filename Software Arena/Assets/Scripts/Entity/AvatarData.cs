using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// entity to keep user's avatar infor and shop state
/// </summary>
public class AvatarData
{
    /// <value>
	/// id of user's current avatar
	/// </value>
    public int avatar_id;
    /// <value>
	/// list of possible avatars, including prices and purchase state
	/// </value>
    public List<weapon> shoplist;

    /// <summary>
    /// constructor to initilise avatar data object
    /// </summary>
    public AvatarData()
    {


    }

    // factory create for default data
    /// <value>
	/// factory method to generate a default avatar data
	/// </value>
    public static AvatarData getDefault()
    {
        // file loading from json file shipped with the app
        // as data is saved to firebase now, no one should override the default data
        // TODO: add load default data from file
        // remove testing code below
        AvatarData defaultData = new AvatarData();
        defaultData.avatar_id = 1;

        List<weapon> list = new List<weapon>();
        weapon w1 = new weapon();
        w1.weapon_name = "hero1";
        w1.weapon_id = 1;
        w1.weapon_img_name = "1.png";
        w1.weapon_price = 1000;
        w1.bought = true;
        weapon w2 = new weapon();
        w2.weapon_name = "hero2";
        w2.weapon_id = 2;
        w2.weapon_img_name = "1.png";
        w2.weapon_price = 1000;
        w2.bought = false;
        weapon w3 = new weapon();
        w3.weapon_name = "hero3";
        w3.weapon_id = 3;
        w3.weapon_img_name = "1.png";
        w3.weapon_price = 1000;
        w3.bought = false;
        weapon w4 = new weapon();
        w4.weapon_name = "hero4";
        w4.weapon_id = 4;
        w4.weapon_img_name = "1.png";
        w4.weapon_price = 1000;
        w4.bought = false;
        weapon w5 = new weapon();
        w5.weapon_name = "hero5";
        w5.weapon_id = 5;
        w5.weapon_img_name = "1.png";
        w5.weapon_price = 1050;
        weapon w6 = new weapon();
        w6.weapon_name = "hero6";
        w6.weapon_id = 6;
        w6.weapon_img_name = "1.png";
        w6.weapon_price = 1100;
        weapon w7 = new weapon();
        w7.weapon_name = "hero7";
        w7.weapon_id = 7;
        w7.weapon_img_name = "1.png";
        w7.weapon_price = 1150;
        weapon w8 = new weapon();
        w8.weapon_name = "hero8";
        w8.weapon_id = 8;
        w8.weapon_img_name = "1.png";
        w8.weapon_price = 1200;
        weapon w9 = new weapon();
        w9.weapon_name = "hero9";
        w9.weapon_id = 9;
        w9.weapon_img_name = "1.png";
        w9.weapon_price = 1250;
        weapon w10 = new weapon();
        w10.weapon_name = "hero10";
        w10.weapon_id = 10;
        w10.weapon_img_name = "1.png";
        w10.weapon_price = 1300;
        w10.bought = false;
        weapon w11 = new weapon();
        w11.weapon_name = "hero11";
        w11.weapon_id = 11;
        w11.weapon_img_name = "1.png";
        w11.weapon_price = 1350;
        w11.bought = false;
        weapon w12 = new weapon();
        w12.weapon_name = "hero12";
        w12.weapon_id = 12;
        w12.weapon_img_name = "1.png";
        w12.weapon_price = 1400;
        w12.bought = false;
        weapon w13 = new weapon();
        w13.weapon_name = "hero13";
        w13.weapon_id = 13;
        w13.weapon_img_name = "1.png";
        w13.weapon_price = 1450;
        w13.bought = false;
        weapon w14 = new weapon();
        w14.weapon_name = "hero14";
        w14.weapon_id = 14;
        w14.weapon_img_name = "1.png";
        w14.weapon_price = 1500;
        w14.bought = false;
        weapon w15 = new weapon();
        w15.weapon_name = "hero15";
        w15.weapon_id = 15;
        w15.weapon_img_name = "1.png";
        w15.weapon_price = 1550;
        w15.bought = false;
        weapon w16 = new weapon();
        w16.weapon_name = "hero16";
        w16.weapon_id = 16;
        w16.weapon_img_name = "1.png";
        w16.weapon_price = 1600;
        w16.bought = false;
        weapon w17 = new weapon();
        w17.weapon_name = "hero17";
        w17.weapon_id = 17;
        w17.weapon_img_name = "1.png";
        w17.weapon_price = 1650;
        w17.bought = false;
        weapon w18 = new weapon();
        w18.weapon_name = "hero18";
        w18.weapon_id = 18;
        w18.weapon_img_name = "1.png";
        w18.weapon_price = 1700;
        w18.bought = false;
        weapon w19 = new weapon();
        w19.weapon_name = "hero19";
        w19.weapon_id = 19;
        w19.weapon_img_name = "1.png";
        w19.weapon_price = 1750;
        w19.bought = false;
        weapon w20 = new weapon();
        w20.weapon_name = "hero20";
        w20.weapon_id = 20;
        w20.weapon_img_name = "1.png";
        w20.weapon_price = 1800;
        w20.bought = false;
        weapon w21 = new weapon();
        w21.weapon_name = "hero21";
        w21.weapon_id = 21;
        w21.weapon_img_name = "1.png";
        w21.weapon_price = 1850;
        w21.bought = false;
        weapon w22 = new weapon();
        w22.weapon_name = "hero22";
        w22.weapon_id = 22;
        w22.weapon_img_name = "1.png";
        w22.weapon_price = 1900;
        w22.bought = false;
        weapon w23 = new weapon();
        w23.weapon_name = "hero23";
        w23.weapon_id = 23;
        w23.weapon_img_name = "1.png";
        w23.weapon_price = 2000;
        w23.bought = false;
        weapon w24 = new weapon();
        w24.weapon_name = "hero24";
        w24.weapon_id = 24;
        w24.weapon_img_name = "1.png";
        w24.weapon_price = 2050;
        w24.bought = false;
        weapon w25 = new weapon();
        w25.weapon_name = "hero25";
        w25.weapon_id = 25;
        w25.weapon_img_name = "1.png";
        w25.weapon_price = 2100;
        w25.bought = false;
        weapon w26 = new weapon();
        w26.weapon_name = "hero26";
        w26.weapon_id = 26;
        w26.weapon_img_name = "1.png";
        w26.weapon_price = 2150;
        w26.bought = false;
        weapon w27 = new weapon();
        w27.weapon_name = "hero27";
        w27.weapon_id = 27;
        w27.weapon_img_name = "1.png";
        w27.weapon_price = 2200;
        w27.bought = false;
        weapon w28 = new weapon();
        w28.weapon_name = "hero28";
        w28.weapon_id = 28;
        w28.weapon_img_name = "1.png";
        w28.weapon_price = 2250;
        w28.bought = false;
        weapon w29 = new weapon();
        w29.weapon_name = "hero29";
        w29.weapon_id = 29;
        w29.weapon_img_name = "1.png";
        w29.weapon_price = 2300;
        w29.bought = false;

        list.Add(w1);
        list.Add(w2);
        list.Add(w3);
        list.Add(w4);
        list.Add(w5);
        list.Add(w6);
        list.Add(w7);
        list.Add(w8);
        list.Add(w9);
        list.Add(w10);
        list.Add(w11);
        list.Add(w12);
        list.Add(w13);
        list.Add(w14);
        list.Add(w15);
        list.Add(w16);
        list.Add(w17);
        list.Add(w18);
        list.Add(w19);
        list.Add(w20);
        list.Add(w21);
        list.Add(w22);
        list.Add(w23);
        list.Add(w24);
        list.Add(w25);
        list.Add(w26);
        list.Add(w27);
        list.Add(w28);
        list.Add(w29);

        defaultData.shoplist = list;
        return defaultData;
    }

}