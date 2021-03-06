using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GameController : MonoBehaviourPun
{
    private PhotonView myPhotonView;
    //reisize answer buttons according to layout
    public SimpleObjectPool answerButtonObjectPool;
  
    public Text questionText;
    
    public Text scoreDisplay;
    //timer countdown to be displayed
    public float timeDisplay;

    //layout
    public Transform answerButtonParent;
    public Text opponentScoreDisplay;
    public GameObject questionDisplay;
    public GameObject WinroundEndDisplay;
    public GameObject LoseroundEndDisplay;
    public GameObject GetCorrectDisplay;
    public GameObject GetWrongDisplay;
    public GameObject OpponentGetCorrectDisplay;
    public GameObject OpponentGetWrongDisplay;
    //high score is displayed after game ends
    public Text highScoreDisplay;
    public Text timeRemainingDisplayText;
  
    private DataController dataController; // responsible for getting question and answer data from database
    private RoundData currentRoundData; // round number
    private QuestionData[] questionPool; //question number

    //menu scene
    [SerializeField]
    private int menuSceneIndex;

    private bool isRoundActive = false; // condition to end the game
    private float timeRemaining; //timer countdown
    private int playerScore;
    private int questionIndex;//index of question in round
    private int opponentScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();


    //initialize
    void Start()
    {
        dataController = FindObjectOfType<DataController>();                                // Store a reference to the DataController so we can request the data we need for this round

        currentRoundData = dataController.GetCurrentRoundData();                            // Ask the DataController for the data for the current round. At the moment, we only have one round - but we could extend this
        questionPool = currentRoundData.questions;                                            // Take a copy of the questions so we could shuffle the pool or drop questions from it without affecting the original RoundData object

        timeRemaining = currentRoundData.timeLimitInSeconds;                                // Set the time limit for this round based on the RoundData object
        playerScore = 0;
        opponentScore = 0;
        questionIndex = 0;
        myPhotonView = GetComponent<PhotonView>();
        myPhotonView.RPC("ShowQuestion", RpcTarget.All,questionIndex);
        isRoundActive = true;
    }

    //display same question for 2 clients
    [PunRPC]
    void ShowQuestion(int whichquestion)
    {
        isRoundActive = true;
        while (answerButtonGameObjects.Count > 0)                                            // Return all spawned AnswerButtons to the object pool
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }

        QuestionData questionData = questionPool[whichquestion];                            // Get the QuestionData for the current question
        questionText.text = questionData.questionText;                                        // Update questionText with the correct text

        for (int i = 0; i < questionData.answers.Length; i++)                                // For every AnswerData in the current QuestionData...
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();            // Spawn an AnswerButton from the object pool
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(questionData.answers[i]);                                    // Pass the AnswerData to the AnswerButton so the AnswerButton knows what text to display and whether it is the correct answer
        }
    }


    //call methods upon answer button click
    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;                    // If the AnswerButton that was clicked was the correct answer, add points
            scoreDisplay.text = playerScore.ToString();
            GetCorrect();
            myPhotonView = GetComponent<PhotonView>();
            myPhotonView.RPC("IncreaseOpponentScore", RpcTarget.Others); //sync clients

        }

        else
        {
            playerScore -= currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplay.text = playerScore.ToString();
            GetWrong();
            myPhotonView = GetComponent<PhotonView>();
            myPhotonView.RPC("DecreaseOpponentScore", RpcTarget.Others);//sync clients
        }


        if (questionPool.Length > questionIndex + 1)                                            // If there are more questions, show the next question
        {
            questionIndex++;
            myPhotonView.RPC("IncreaseOpponentIndex", RpcTarget.Others);
            myPhotonView.RPC("ShowQuestion", RpcTarget.All, questionIndex);//sync clients

        }
        
        else                                                         // If there are no more questions, the round ends
        {
            if (opponentScore > playerScore)
            {
                LoseEndRound();
                myPhotonView = GetComponent<PhotonView>();
                myPhotonView.RPC("WinEndRound", RpcTarget.Others);//sync clients
            }
            else
            {
                WinEndRound();
                myPhotonView = GetComponent<PhotonView>();
                myPhotonView.RPC("LoseEndRound", RpcTarget.Others);//sync clients
            }
            
        }
    }

    //display UI across clients when player press correct button
    async void GetCorrect()
    {
        GetCorrectDisplay.SetActive(true);
        myPhotonView = GetComponent<PhotonView>();
        myPhotonView.RPC("OpponentCorrect", RpcTarget.Others);
        await Task.Delay(1000);
        GetCorrectDisplay.SetActive(false);

    }

     //display UI across clients when player press wrong button
    async void GetWrong()
    {
        GetWrongDisplay.SetActive(true);
        myPhotonView = GetComponent<PhotonView>();
        myPhotonView.RPC("OpponentWrong", RpcTarget.Others);
        await Task.Delay(1000);
        GetWrongDisplay.SetActive(false);
    }

    //display UI across clients when on opponent press correct button
    [PunRPC]
    async void OpponentCorrect()
    {
        OpponentGetCorrectDisplay.SetActive(true);
        await Task.Delay(1000);
        OpponentGetCorrectDisplay.SetActive(false);
    }
    //display UI across clients when on opponent wrong correct button
    [PunRPC]
    async void OpponentWrong()
    {
        OpponentGetWrongDisplay.SetActive(true);
        await Task.Delay(1000);
        OpponentGetWrongDisplay.SetActive(false);
    }

    //display win UI after game ends and display high score
    [PunRPC]
    void WinEndRound()
    {
        isRoundActive = false;

        dataController.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = dataController.GetHighestPlayerScore().ToString();

        questionDisplay.SetActive(false);
        WinroundEndDisplay.SetActive(true);
    }
    //display lose UI after game ends and display high score
    [PunRPC]
    void LoseEndRound()
    {
        isRoundActive = false;

        dataController.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = dataController.GetHighestPlayerScore().ToString();

        questionDisplay.SetActive(false);
        LoseroundEndDisplay.SetActive(true);
    }

    //return to menu after game ends and leave server
    public void ReturnToMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

    //increase and display opponent score 
    [PunRPC]
    void IncreaseOpponentScore()
    {
        opponentScore += currentRoundData.pointsAddedForCorrectAnswer;
        opponentScoreDisplay.text = opponentScore.ToString();
    }
    //decrease and display opponent score 
    [PunRPC]
    void DecreaseOpponentScore()
    {
        opponentScore -= currentRoundData.pointsAddedForCorrectAnswer;
        opponentScoreDisplay.text = opponentScore.ToString();
    }

     //display same question in other clients after player presses a answer button
    [PunRPC] 
    void IncreaseOpponentIndex()
    {
        questionIndex++;
    }

    //update time count down
    private void UpdateTimeRemainingDisplay()
     {
         timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
     }

     // Update is called once per frame
     void Update()
     {
         if (isRoundActive)
         {
             timeRemaining -= Time.deltaTime;
             UpdateTimeRemainingDisplay();

             if (timeRemaining <= 0f)
             {
                 if (opponentScore > playerScore)
                {
                    LoseEndRound();
                    myPhotonView = GetComponent<PhotonView>();
                    myPhotonView.RPC("WinEndRound", RpcTarget.Others);//opponent win
                }
                else
                {
                    WinEndRound();
                    myPhotonView = GetComponent<PhotonView>();
                    myPhotonView.RPC("LoseEndRound", RpcTarget.Others); //opponent lose

                }
             }

         }
     }

}