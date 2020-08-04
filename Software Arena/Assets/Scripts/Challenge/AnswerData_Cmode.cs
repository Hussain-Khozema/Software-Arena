
using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnswerData_Cmode
{
    public string answerText;
    public bool isCorrect;

    public AnswerData_Cmode(string s, bool b) {
        answerText = s;
        isCorrect = b;
    }
}