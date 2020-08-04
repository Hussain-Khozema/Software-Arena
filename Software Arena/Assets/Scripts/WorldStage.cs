using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class WorldStage : MonoBehaviour
{
    public int world, stage;
    public GameObject silver,gold,platinum,diamond;
    // Start is called before the first frame update

    // This fuction will unlock and change sprite depending on the world 
    // If player completed all the stages in any world it will unlock the subsequent world
    // Start is called before the first frame update
    void Start()
    {
        string hh = PlayerPrefs.GetString("overview");
        world = Int32.Parse(hh.Substring(0, 1));
        stage = Int32.Parse(hh.Substring(2, 1));

        switch (world)
        {
            case 2:
                silver.SetActive(false);
                break;
            case 3:
                silver.SetActive(false);
                gold.SetActive(false);
                break;
            case 4:
                silver.SetActive(false);
                gold.SetActive(false);
                platinum.SetActive(false);
                break;
            case 5:
                silver.SetActive(false);
                gold.SetActive(false);
                platinum.SetActive(false);
                diamond.SetActive(false);
                break;
        }

        GameObject.Find("Bronze").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(1));
        GameObject.Find("Silver").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(2));
        GameObject.Find("Gold").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(3));
        GameObject.Find("Platinum").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(4));
        GameObject.Find("Diamond").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(5));

    }

    // This function will set which world and stage player have choose
    void ButtonClicked(int a)
    {
        PlayerPrefs.SetString("selectedWorld", a + "-" + stage);
        PlayerPrefs.SetString("originalState", world + "-" + stage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
