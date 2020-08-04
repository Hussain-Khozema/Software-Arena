using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButton_cmode : MonoBehaviour
{

    public Text answerText;

    private AnswerData_Cmode answerData;
    private GameController_cmode gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController_cmode>();
    }

    public void Setup(AnswerData_Cmode data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }


    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }
}