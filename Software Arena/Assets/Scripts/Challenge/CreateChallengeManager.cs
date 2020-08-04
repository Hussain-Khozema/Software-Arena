using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CreateChallengeManager : MonoBehaviour
{
    public Text[] t;
    public GameObject popup;
    public Button submitButton;

    public void Submit()
    {
        // checking empty fields
        if (string.IsNullOrEmpty(t[0].text))
        {
            Debug.LogError("Question is empty! ");
            return;
        }
        if (string.IsNullOrEmpty(t[1].text))
        {
            Debug.LogError("Answer is empty! ");
            return;
        }

        // creating question object to be uploaded
        List<string> texts = new List<string>();
        texts.Add(t[1].text);
        for (int i = 2; i < 5; i++)
        {
            if (!String.IsNullOrEmpty(t[i].text))
            {
                texts.Add(t[i].text);
            }
        }
        // string question_text, List< string > choices, int correct_choice
        ChallengeQuestion c = new ChallengeQuestion(t[0].text, texts, 0);
        c.swapChoices();

        // uploading to firebase
        FirebaseManager.getInstance().addQuestion(c, (result, msg) =>
        {
            Debug.Log("Save Question: " + result + ", " + msg);
            if (result)
            {
                Clear();
                popup.SetActive(true);
                t[0].text = "Added to database!";
                Button btn = submitButton.GetComponent<Button>();
		        btn.onClick.AddListener(setScene);
            }
            else
            {
                Debug.LogWarning("Save Question failed from database!");
            }
        });
        
    }

    public void setScene(){
        SceneManager.LoadScene(16);
    }

    public void Clear()
    {
        foreach (Text text in t)
        {
            text.text = "";
        }
    }


    // return to teacher or student menu
    public void BackButton() {
        FirebaseManager.getInstance().getUserInfo((user, msg) => {
            if (user != null) {
                Debug.Log("doCheckUserRole: " + user.role);
                // redirect to diff scene
                if (user.role.Equals("teacher")) {
                    SceneManager.LoadScene("teacher_menu");
                } else {
                    SceneManager.LoadScene("Main Menu");
                }
            } else { 
                SceneManager.LoadScene("Main Menu");
            }
        });
    }
}
