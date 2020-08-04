using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class stageGold : MonoBehaviour
{
    public Sprite Complete;
    public GameObject Gstage2, Gstage3, Gstage4, Gstage5;
    private int world, stage, sworld;
    public string hh, ll;

    // This fuction will unlock and change sprite depending on the world and stage that they are currently in
    // For example if player is beyond world 1 all the stages will be unlock OR if player is in this stage but 
    // yet to complete the remaining stage certain will not be unlocked
    // Start is called before the first frame update

    void Start()
    {
        hh = PlayerPrefs.GetString("originalState");
        ll = PlayerPrefs.GetString("selectedWorld");
        world = Int32.Parse(hh.Substring(0, 1));
        stage = Int32.Parse(hh.Substring(2, 1));
        sworld = Int32.Parse(ll.Substring(0, 1));

        if (world > 3)
        {
            Gstage2.SetActive(false);
            Gstage3.SetActive(false);
            Gstage4.SetActive(false);
            Gstage5.SetActive(false);

            GameObject.Find("Stage 1").GetComponent<Image>().sprite = Complete;
            GameObject.Find("Stage 2").GetComponent<Image>().sprite = Complete;
            GameObject.Find("Stage 3").GetComponent<Image>().sprite = Complete;
            GameObject.Find("Stage 4").GetComponent<Image>().sprite = Complete;
            GameObject.Find("Stage 5").GetComponent<Image>().sprite = Complete;

            GameObject.Find("Stage 5").GetComponent<Image>().sprite = Complete;
            GameObject.Find("Stage 1").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(1));
            GameObject.Find("Stage 2").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(2));
            GameObject.Find("Stage 3").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(3));
            GameObject.Find("Stage 4").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(4));
            GameObject.Find("Stage 5").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(5));
        }
        else
        {
            switch (stage)
            {
                case 2:
                    GameObject.Find("Stage 1").GetComponent<Image>().sprite = Complete;

                    Gstage2.SetActive(false);
                    break;
                case 3:
                    GameObject.Find("Stage 1").GetComponent<Image>().sprite = Complete;
                    GameObject.Find("Stage 2").GetComponent<Image>().sprite = Complete;
                    Gstage2.SetActive(false);
                    Gstage3.SetActive(false);
                    break;
                case 4:
                    GameObject.Find("Stage 1").GetComponent<Image>().sprite = Complete;
                    GameObject.Find("Stage 2").GetComponent<Image>().sprite = Complete;
                    GameObject.Find("Stage 3").GetComponent<Image>().sprite = Complete;
                    Gstage2.SetActive(false);
                    Gstage3.SetActive(false);
                    Gstage4.SetActive(false);
                    break;
                case 5:
                    GameObject.Find("Stage 1").GetComponent<Image>().sprite = Complete;
                    GameObject.Find("Stage 2").GetComponent<Image>().sprite = Complete;
                    GameObject.Find("Stage 3").GetComponent<Image>().sprite = Complete;
                    GameObject.Find("Stage 4").GetComponent<Image>().sprite = Complete;
                    Gstage2.SetActive(false);
                    Gstage3.SetActive(false);
                    Gstage4.SetActive(false);
                    Gstage5.SetActive(false);
                    break;
            }

            GameObject.Find("Stage 1").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(1));
            GameObject.Find("Stage 2").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(2));
            GameObject.Find("Stage 3").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(3));
            GameObject.Find("Stage 4").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(4));
            GameObject.Find("Stage 5").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(5));
        }
        
    }

    // This function will set which world and stage player have choose
    void ButtonClicked(int a)
    {
        PlayerPrefs.SetString("stageLevel", sworld + "-" + a);
        PlayerPrefs.SetString("originalState", hh);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
