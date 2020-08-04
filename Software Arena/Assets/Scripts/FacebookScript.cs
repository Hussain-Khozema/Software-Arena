using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookScript : MonoBehaviour
{
    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    //public GameObject DialogUsername;
    //public GameObject DialogProfilePic;
    private void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }

    private void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }
        DealWithFBMenus(FB.IsLoggedIn);
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBlogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
        
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
            }
            else
            {
                Debug.Log("FB is not logged in");
            }
            DealWithFBMenus(FB.IsLoggedIn);

        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);

            //FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            //FB.API("/me/picture?type=square&heigh=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }
    public void FacebookLogout()
    {
        FB.LogOut();
        Debug.Log("Facebook is logged out");
        DialogLoggedIn.SetActive(false);
        DialogLoggedOut.SetActive(true);
    }
    //void DisplayUsername(IResult result)
    //{
    //    Text UserName = DialogUsername.GetComponent<Text>();
    //    if (result.Error == null)
    //    {
    //        UserName.text = "Hi," + result.ResultDictionary["first_name"];
    //    }
    //    else
    //    {
    //        Debug.Log(result.Error);
    //    }
    //}

    //void DisplayProfilePic(IGraphResult result)
    //{
    //    if (result.Texture != null)
    //    {
    //        Image ProfilePic = DialogProfilePic.GetComponent<Image>();
    //        ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
    //    }

    //}
}
