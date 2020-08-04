using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//template used to change the text of each report-button generated
public class rlButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    
    public void SetText(string textString)
    {
        myText.text = textString;
    }

}
