using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ForgetPassword : MonoBehaviour
{
    public InputField inputAcc;
    public GameObject popupforgetpassword;
    public GameObject popup;
    public Button btnForget;
    public Button submitButton;
    public string mytext;

    // Start is called before the first frame update
    void Start()
    {
        // Following will be run when forget password button is clicked
        btnForget.onClick.AddListener(
        delegate
        {
        popupforgetpassword.SetActive(true);
        Button btn = submitButton.GetComponent<Button>();
		btn.onClick.AddListener(doResetPassword);
        }
    );

    }
    // This function will request to reset the password of the user
    void doResetPassword()
    {
        mytext = inputAcc.text;
        FirebaseManager fbMgr = FirebaseManager.getInstance();
        fbMgr.forgetPassword(mytext);
        popupforgetpassword.SetActive(false);
        popup.SetActive(true);
    }
}
