﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class LogoutStudent : MonoBehaviour
{
    public GameObject popUpExit;
    public Button yes;
    public Button logout;
    public Button no;

    void Start()
    {
        logout.onClick.AddListener(
            delegate {
                // TODO: validate input is not null!!! 
                forgetPassword();
            }
        );

    }
    // Update is called once per frame
    public void forgetPassword()
    {
        popUpExit.SetActive(true);
        yes.onClick.AddListener(scene0);
        no.onClick.AddListener(scene2);
    }

    public void scene0(){
        UnitySceneManager.LoadScene(0);
    }
    public void scene2(){
        UnitySceneManager.LoadScene(2);
    }
}