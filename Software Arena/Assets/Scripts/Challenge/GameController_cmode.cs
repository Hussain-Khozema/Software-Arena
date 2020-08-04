using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController_cmode : MonoBehaviour {


    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text finalScoreDisplayText;
    public Text username;
    public SimpleObjectPool_cmode answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;

    private List<ChallengeQuestion> cqs;

    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private const int POINT_AWARD = 10;
    private AppData appdata;

    // Get current player's gamedata
    void Awake() {

        FirebaseManager.getInstance().getGameData((result, msg) => {
            if (result == null) {
                print("Get user data from database failed!");
            } else {
                appdata = result;
                Debug.Log(appdata.name);
                username.text = appdata.name;
                change_display_char cdc = GetComponent<change_display_char>();
                cdc.UpdateChar(appdata.avatar.avatar_id);
                
            }
        });


        playerScore = 0;

        questionDisplay.SetActive(true);
        roundEndDisplay.SetActive(false);

        // fetch all challenge questions marked with "Submitted"
        cqs = new List<ChallengeQuestion>();
        FirebaseManager.getInstance().getQuestionList((result, msg) => {
            if (null == result) {
                Debug.LogError("Get question failed!");

            } else {
                foreach (ChallengeQuestion q in result) {
                    if (q.status == ChallengeQuestion.Status.Submitted) {
                        cqs.Add(q);
                    }
                }
                Debug.Log("Got questions: " + cqs.Count);
                DisplayNextQuestion();
            }
        });

    }

    // show next question
    private void DisplayNextQuestion() {
        if (cqs.Count < 1) {
            EndChallenge();
            return;
        }
        ChallengeQuestion cq = cqs[0];
        ShowQuestion(cq);
        cqs.RemoveAt(0);

    }
    private void ShowQuestion(ChallengeQuestion questionData) {
        RemoveAnswerButtons();
        questionDisplayText.text = questionData.description;

        for (int i = 0; i < 4; i++) {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton_cmode answerButton = answerButtonGameObject.GetComponent<AnswerButton_cmode>();
            answerButton.Setup(new AnswerData_Cmode(questionData.choices[i], i == questionData.idxAnswer));

        }
    }

    private void RemoveAnswerButtons() {
        while (answerButtonGameObjects.Count > 0) {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    // UI action on choosing one of the answer
    public void AnswerButtonClicked(bool isCorrect) {
        if (isCorrect) {
            playerScore += POINT_AWARD;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }
        DisplayNextQuestion();
    }

    // End challenge and upload score
    public void EndChallenge() {

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
        finalScoreDisplayText.text = "Score: " + playerScore.ToString();

        FirebaseManager.getInstance().getGameData((result, msg) => {
            if (result == null) {
                print("Get user data from database failed!");
            } else {
                result.challengePoints = playerScore;
                DataManager.getInstance().updateGameData(result, (resultBool, message) => {
                    if (resultBool) {
                        Debug.Log("Update high score challenge mode SUCCESS");
                    } else {
                        Debug.LogWarning("Update high score challenge mode failed from database! " + message);
                    }
                });
            }
        });

    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("CMenuScreen");
    }

}