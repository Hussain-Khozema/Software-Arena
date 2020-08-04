using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterUserButtonAction : MonoBehaviour
{
    public GameObject popupNoUserRecord;
    public GameObject popupSuccessful;
    public GameObject mybtn;
    public Sprite sprite;
    public Text mytext;
    public Text mytext_1;
    const string TAG = "regBtnAction";
    public InputField inputUsername, inputPass, inputName; // add checkbox for role
    public Button btnRegister;

    GameProgress gp = new GameProgress();
    StageStats ss = new StageStats();
    AppData ad = new AppData();
    AvatarData av = new AvatarData();
    private string role;

    // Start is called before the first frame update
    void Start()
    {
        btnRegister.onClick.AddListener(
            delegate {
                // validate if input is not null
                doRegister();
            }
        );
    }

    // This function will be called when user clicks to register
    void doRegister()
    {
        sprite = mybtn.GetComponent<Image>().sprite;
        if( (sprite.ToString().Substring(0,(sprite.ToString().Length)-21)).Equals("check_off_tiny"))
        {
            role = "student";
        }
        else
        {
            role = "teacher";
        }
        string username = inputUsername.text;
        string password = inputPass.text;
        string name = inputName.text;
        FirebaseManager fbMgr = FirebaseManager.getInstance();
        fbMgr.doRegister(username, password, (result, msg) =>
        {
            Debug.Log(TAG + ":: doRegister:" + result + ", " + msg);
            if (result == true)
            {
                User newUser = new User(username, name, role);
                fbMgr.updateUserProfile(name);
                fbMgr.saveUser(newUser);
                doSendEmailVerification();
                
                if(string.Compare(role,"student") == 0)
                {
                    createGameData(name);
                }
            }
            else
            {
                    Mainthread.wkr.AddJob(() =>
                    {
                        mytext.text = msg;
                        // Will run on main thread, hence issue is solved
                        popupNoUserRecord.SetActive(true);
                    });
            }
        });


    }


    // This function is to create new game data with a default value when the player sign up for the game
    //  game progress and avatar are created at default
    void createGameData(String username)
    {
        ad.name = username;
        ad.charPoints = 1500;
        ad.challengePoints = 0;
        ad.multiPlayerPoints = 0;
        gp.curWorld = 1;
        gp.curStage = 1;
        ad.progress = gp;
        ad.avatar = AvatarData.getDefault();

        var fbMgr = DataManager.getInstance();
        fbMgr.updateGameData(ad, (result, msg) =>
        {

            if (result)
            {
                print("SUCCESS SAVED DATA");
                // TODO: prompt add succeed
            }
            else
            {
                Debug.LogWarning("Save Question failed from database!");
            }
        });
    }

    // This function sends email verification for students upon registering
    private void doSendEmailVerification()
    {
        FirebaseManager fbMgr = FirebaseManager.getInstance();

        fbMgr.sendVerificaitionEmail((result, msg) => {
            Debug.Log(TAG + ":: doSendEmailVerification:" + result + ", " + msg);
            if (result)
            {
                // show dialog to ask user verify email
                // after user click confirm, direct back to main page
                Mainthread.wkr.AddJob(() =>
                {
                    mytext_1.text = msg;
                    // Will run on main thread, hence issue is solved
                    popupSuccessful.SetActive(true);
                });
            }
        });
    }
}
