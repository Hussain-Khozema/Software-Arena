using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginButtonAction : MonoBehaviour
{
    public InputField inputAcc, inputPass;
    public Button btnLogin;
    public GameObject popupNoUserRecord;
    public Text mytext;
    void Start()
    {
        btnLogin.onClick.AddListener(
            delegate {
                // TODO: validate input is not null!!! 
                doLogin();
            }
        );

    }

    // This function will be called when user clicks login
    void doLogin()
    {
        string email = inputAcc.text;
        string password = inputPass.text;
        FirebaseManager fbMgr = FirebaseManager.getInstance();
        fbMgr.doLogin(email, password, (result, msg) =>
        {
            Debug.Log("doLogin: " + result + ", " + msg);
            if (result == false)
            {
                mytext.text = msg;
                popupNoUserRecord.SetActive(true);
            }

            if (result==true)
            {

                Debug.Log("login sucess, check user role");
                // TODO: decide student or teacher menu

                doCheckUserRole();

            }
            // show error message
        });
    }

    // This function checks whether the user is a student or teacher
    void doCheckUserRole()
    {
        FirebaseManager fbMgr = FirebaseManager.getInstance();
        fbMgr.getUserInfo((user, msg) =>
        {
            if (user != null)
            {
                Debug.Log("doCheckUserRole: " + user.role);
                // redir to diff scene
                if (user.role.Equals("teacher"))
                {
                    SceneManager.LoadScene(3);
                } else
                {
                    SceneManager.LoadScene(2);
                }
                
            }
            // else show error message

        });
    }
}