using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
public class BackButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public GameObject popUpExit;
    public Button yes;
    public Button no;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {//When a key is pressed down it see if it was the escape key if it was it will execute the code
            if (UnitySceneManager.GetActiveScene().buildIndex == 0){
                popUpExit.SetActive(true);
                yes.onClick.AddListener(exit);
                no.onClick.AddListener(scene0);
                }
            else if(UnitySceneManager.GetActiveScene().buildIndex == 1)
                UnitySceneManager.LoadScene(0);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 2){
                popUpExit.SetActive(true);
                yes.onClick.AddListener(scene0);
                no.onClick.AddListener(scene2);
        }
            else if(UnitySceneManager.GetActiveScene().buildIndex == 3){
                popUpExit.SetActive(true);
                yes.onClick.AddListener(scene0);
                no.onClick.AddListener(scene3);
                }
            else if(UnitySceneManager.GetActiveScene().buildIndex == 4)
                UnitySceneManager.LoadScene(2);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 5)
                UnitySceneManager.LoadScene(4);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 6)
                UnitySceneManager.LoadScene(5);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 7)
                UnitySceneManager.LoadScene(6);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 8)
                UnitySceneManager.LoadScene(7);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 9)
                UnitySceneManager.LoadScene(8);
            else if(UnitySceneManager.GetActiveScene().buildIndex == 14)
                UnitySceneManager.LoadScene(2);
        }
    }

    public void exit(){
        Application.Quit();
    }

    public void scene0(){
        UnitySceneManager.LoadScene(0);
    }
    public void scene2(){
        UnitySceneManager.LoadScene(2);
    }
    public void scene3(){
        UnitySceneManager.LoadScene(3);
    }
}
