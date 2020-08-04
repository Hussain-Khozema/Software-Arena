using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stage : MonoBehaviour
{
    public GameObject Bstage2, Bstage3, Bstage4, Bstage5;
   /* public GameObject Sstage2, Sstage3, Sstage4, Sstage5;
    public GameObject Gstage2, Gstage3, Gstage4, Gstage5;
    public GameObject Pstage2, Pstage3, Pstage4, Pstage5;
    public GameObject Dstage2, Dstage3, Dstage4, Dstage5;*/

    void Start()
    {
        string hh = PlayerPrefs.GetString("currentStage");
        print("STAGELEVEL::::: " + hh + " :::::::");
        int world = Int32.Parse(hh.Substring(0, 1));
        int stage = Int32.Parse(hh.Substring(2, 1));

        switch (stage)
        {
            case 2:
                Bstage2.SetActive(false);
                break;
            case 3:
                Bstage2.SetActive(false);
                Bstage3.SetActive(false);
                break;
            case 4:
                Bstage2.SetActive(false);
                Bstage3.SetActive(false);
                Bstage4.SetActive(false);
                break;
            case 5:
                Bstage2.SetActive(false);
                Bstage3.SetActive(false);
                Bstage4.SetActive(false);
                Bstage5.SetActive(false);
                break;
        }
    }
    // Start is called before the first frame update
    /*void Start()
    {
        string hh = PlayerPrefs.GetString("currentStage");
        print("STAGELEVEL::::: " + hh + " :::::::");
        int world = Int32.Parse(hh.Substring(0, 1));
        int stage = Int32.Parse(hh.Substring(2, 1));

        print("pppppppppppppppppp " + world);
        switch (world)
        {
            case 0:
                print("IN CASE 0");
                switch (stage)
                {
                    case 2:
                        Bstage2.SetActive(false);
                        break;
                    case 3:
                        Bstage2.SetActive(false);
                        Bstage3.SetActive(false);
                        break;
                    case 4:
                        Bstage2.SetActive(false);
                        Bstage3.SetActive(false);
                        Bstage4.SetActive(false);
                        break;
                    case 5:
                        Bstage2.SetActive(false);
                        Bstage3.SetActive(false);
                        Bstage4.SetActive(false);
                        Bstage5.SetActive(false);
                        break;
                }
                break;
            case 1:
                print("IN CASE 1");
                switch (stage)
                {
                    case 2:
                        Sstage2.SetActive(false);
                        break;
                    case 3:
                        Sstage2.SetActive(false);
                        Sstage3.SetActive(false);
                        break;
                    case 4:
                        Sstage2.SetActive(false);
                        Sstage3.SetActive(false);
                        Sstage4.SetActive(false);
                        break;
                    case 5:
                        Sstage2.SetActive(false);
                        Sstage3.SetActive(false);
                        Sstage4.SetActive(false);
                        Sstage5.SetActive(false);
                        break;
                }
                break;
            case 2:
                print("IN CASE 2");
                switch (stage)
                {
                    case 2:
                        Gstage2.SetActive(false);
                        break;
                    case 3:
                        Gstage2.SetActive(false);
                        Gstage3.SetActive(false);
                        break;
                    case 4:
                        Gstage2.SetActive(false);
                        Gstage3.SetActive(false);
                        Gstage4.SetActive(false);
                        break;
                    case 5:
                        Gstage2.SetActive(false);
                        Gstage3.SetActive(false);
                        Gstage4.SetActive(false);
                        Gstage5.SetActive(false);
                        break;
                }
                break;
            case 3:
                switch (stage)
                {
                    case 2:
                        Pstage2.SetActive(false);
                        break;
                    case 3:
                        Pstage2.SetActive(false);
                        Pstage3.SetActive(false);
                        break;
                    case 4:
                        Pstage2.SetActive(false);
                        Pstage3.SetActive(false);
                        Pstage4.SetActive(false);
                        break;
                    case 5:
                        Pstage2.SetActive(false);
                        Pstage3.SetActive(false);
                        Pstage4.SetActive(false);
                        Pstage5.SetActive(false);
                        break;
                }
                break;
            case 4:
                switch (stage)
                {
                    case 2:
                        Dstage2.SetActive(false);
                        break;
                    case 3:
                        Dstage2.SetActive(false);
                        Dstage3.SetActive(false);
                        break;
                    case 4:
                        Dstage2.SetActive(false);
                        Dstage3.SetActive(false);
                        Dstage4.SetActive(false);
                        break;
                    case 5:
                        Dstage2.SetActive(false);
                        Dstage3.SetActive(false);
                        Dstage4.SetActive(false);
                        Dstage5.SetActive(false);
                        break;
                }
                break;
        }
        
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
