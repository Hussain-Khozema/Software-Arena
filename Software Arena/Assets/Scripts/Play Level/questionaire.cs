using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class questionaire : MonoBehaviour
{
    // Game objects used 
    public GameObject popupPanelCorrect;
    public GameObject popupPanelWrong, popupPanelComplete;
    public GameObject changeQuestionBtn, nextLvlButton;
    public GameObject testButton;

    // Question list get/set 
    public QuestionsList QuestionsList { get; private set; }
    
    // Variables used for checking and allocation
    public int count = 0, qCount;
    int aa, bb, cc, dd,repeat = 0,point,lvlPoints= 0;
    bool ee, ff, gg, hhh;

    // Variables used for world, stage and character points
    public int prefWorld, prefStage, oriWorld, oriStage;

    // Object to store game progress
    GameProgress gp = new GameProgress();
    StageStats ss = new StageStats();
    AppData ad = new AppData();
    AppData data;

    // Start is called before the first frame update
    // This function will readData() function to pull the latest game progress
    // Game objects with listeners to check button clicked and proceed with the necessary actions
    void Start()
    {
        readData();

        string fromPlayerPrefs = PlayerPrefs.GetString("stageLevel");
        string originalData = PlayerPrefs.GetString("originalState");

        prefWorld = Int32.Parse(fromPlayerPrefs.Substring(0, 1));
        prefStage = Int32.Parse(fromPlayerPrefs.Substring(2, 1));

        oriWorld = Int32.Parse(originalData.Substring(0, 1));
        oriStage = Int32.Parse(originalData.Substring(2, 1));

        qCount = questionChecker();

        nextQuestion(true);

        Button btn = changeQuestionBtn.GetComponent<Button>();
        btn.onClick.AddListener(() => nextQuestion(true));

        Button nxtlvlbtn = nextLvlButton.GetComponent<Button>();
        nxtlvlbtn.onClick.AddListener(() => nextLevel());
        
        Text hh = GameObject.Find("leaderPoints").GetComponent<Text>();
        hh.text = "" + (data.challengePoints + data.multiPlayerPoints) + "";

        GameObject.Find("option_0").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(aa, ee));
        GameObject.Find("option_1").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(bb, ff));
        GameObject.Find("option_2").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(cc, gg));
        GameObject.Find("option_3").GetComponent<Button>().onClick.AddListener(() => ButtonClicked(dd, hhh));
        GameObject.Find("back Button").GetComponent<Button>().onClick.AddListener(() => backButton(prefWorld));
    }

    // Update is called once per frame
    // character points will be updated real time
    void Update()
    {
        Text hh = GameObject.Find("characterPoints").GetComponent<Text>();
        hh.text = "" + data.charPoints + "";
    }

    // This function is to allow user to go back to the rescpective scene
    void backButton(int a)
    {
        switch (a)
        {
            case 1:
                SceneManager.LoadScene(5);
                break;
            case 2:
                SceneManager.LoadScene(9);
                break;
            case 3:
                SceneManager.LoadScene(7);
                break;
            case 4:
                SceneManager.LoadScene(8);
                break;
            case 5:
                SceneManager.LoadScene(6);
                break;
        }
    }


    // This function is to proceed to the next stage and reseting some variables
    void nextLevel()
    {
        popupPanelComplete.SetActive(false);
        count = 0;
        prefStage++;
        nextQuestion(true);
    }
    

    // This function is for the completion of a stage. An assigned pop up will appear indicating the points collected at this stage
    // Parameters needed for the AppData will be updated in this function
    // If player have proceed to a higher level and stage. Their current progress will be updated unless player re-attempt a completed stage
    void nextQuestion(bool w)
    {
        if (w == false)
        {
            popupPanelWrong.SetActive(false);
        }
        else if (w == true)
        {
            if (count != 0 && count == 5)
            {
                popupPanelComplete.SetActive(true);
                Text hh = GameObject.Find("scoreForLevel").GetComponent<Text>();
                hh.text = "Total points earned " + lvlPoints + "!";
                String tt = prefWorld + "-" + prefStage;

                // if selected stage is smaller than the current stage
                // then check the world
                if ((prefWorld != oriWorld) || (prefWorld == oriWorld && prefStage < oriStage))
                {
                   
                        gp.curStage = oriStage;
                        gp.curWorld = oriWorld;
                        gp.stats.Add(tt, ss);
                        ad.progress = gp;
                        ad.charPoints = data.charPoints;

                }
                else
                {
                    if(oriStage == 5 & oriWorld == 5)
                    {
                        gp.curStage = 6;
                    }
                    else
                    {
                        gp.curStage = prefStage + 1;
                        gp.curWorld = prefWorld;
                        gp.stats.Add(tt, ss);
                        if (gp.curStage > 5)
                        {
                            gp.curWorld = prefWorld + 1;
                            gp.curStage = 1;
                        }
                        ad.progress = gp;
                        ad.charPoints = data.charPoints;
                    }
                    
                }

                updateGameData(ad);
            }
            else
            {
                popupPanelCorrect.SetActive(false);
                loadQuestions();
            }
        }
    }


    // This function is use to load question from the json file included in the application
    // Questions will be filtered by the world and stage that player have chosen
    // Assigning of option text to the specific button
    // counter() function will called to update the necessary parameters
    void loadQuestions()
    {

        // loadJson
        TextAsset asset = Resources.Load("Questions") as TextAsset;
        QuestionsList = JsonUtility.FromJson<QuestionsList>(asset.text);

        // stage test
        Text quesCnt = GameObject.Find("questionCount").GetComponent<Text>();
        quesCnt.text = "Stage " + prefStage + " (" + (count + 1) + "\\5)";

        // loop through 
        for (int a = 0; a < QuestionsList.Questions.Count; a++)
        {

            if ((prefStage == QuestionsList.Questions[a].stage && count == QuestionsList.Questions[a].level) && prefWorld == QuestionsList.Questions[a].world)
                {
                    // question text
                    Text qnstxt = GameObject.Find("questionsTxt").GetComponent<Text>();
                    qnstxt.text = QuestionsList.Questions[qCount].qns_text;
                    
                    repeat = 0;

                    if (QuestionsList.Questions[a].choice == 0)
                    {
                        GameObject.Find("option_0").GetComponentInChildren<Text>().text = QuestionsList.Questions[a].answer;
                        aa = QuestionsList.Questions[a].choice;
                        ee = QuestionsList.Questions[a].boolen;
                    }
                    else if (QuestionsList.Questions[a].choice == 1)
                    {
                        GameObject.Find("option_1").GetComponentInChildren<Text>().text = QuestionsList.Questions[a].answer;
                        bb = QuestionsList.Questions[a].choice;
                        ff = QuestionsList.Questions[a].boolen;
                    }
                    else if (QuestionsList.Questions[a].choice == 2)
                    {
                        GameObject.Find("option_2").GetComponentInChildren<Text>().text = QuestionsList.Questions[a].answer;
                        cc = QuestionsList.Questions[a].choice;
                        gg = QuestionsList.Questions[a].boolen;
                    }
                    else if (QuestionsList.Questions[a].choice == 3)
                    {
                        GameObject.Find("option_3").GetComponentInChildren<Text>().text = QuestionsList.Questions[a].answer;
                        dd = QuestionsList.Questions[a].choice;
                        hhh = QuestionsList.Questions[a].boolen;
                    }
                }
        }

        counter();

    }


    // This function is to check if player have answered correctly and an assigned pop up will appear
    void ButtonClicked(int a, bool w)
    {

        if (w == true)
        {
            popupPanelCorrect.SetActive(true);
            Text hh = GameObject.Find("pointsTxt").GetComponent<Text>();
            hh.text = "You earn " + scoring().ToString() + " points";
        }
        else if (w == false)
        {
            repeat++;
            ss.numAttempts += repeat;
            ss.timeClear = DateTime.Now;
            gp.avgAttempts = 15 / 5;

            popupPanelWrong.SetActive(true);
            Button retry = testButton.GetComponent<Button>();
            retry.onClick.AddListener(() => nextQuestion(w));
        }

    }


    // This function is for the level count and qCount is for loading of set of question
    // return count
    int counter()
    {
        count++;
        qCount += 4;
        return count;
    }


    // This function is for the scoring system. By default player will get 100 points and points will reduced by 20 points after each re-attempt
    // Character points will increment based on the points obtained. The level points will increment to display at the success page
    // return point
    int scoring()
    {

        switch(repeat){
            case 0:
                point = 100;
                break;
            case 1:
                point = 80;
                break;
            case 2:
                point = 60;
                break;
            case 3:
                point = 40;
                break;
            case 4:
                point = 20;
                break;
            default:
                point = 0;
                break;
        }

        data.charPoints += point;
        lvlPoints += point;
        return point;
    }


    // This function is to update the game data after completing a stage
    void updateGameData(AppData ap)
    {
        String tt = prefWorld + "-" + prefStage;
        
        ap.avatar = data.avatar;
        ap.name = data.name;
        ap.challengePoints = data.challengePoints;
        ap.multiPlayerPoints = data.multiPlayerPoints;
        DataManager.getInstance().updateGameData(ap);
    }


    // This function is to set which question to load depending on the world and stage selected
    // return cc
    int questionChecker()
    {
        int cc=0;
        if(prefWorld == 1)
        {
            switch (prefStage)
            {
                case 1:
                    cc = 0;
                    break;
                case 2:
                    cc = 20;
                    break;
                case 3:
                    cc = 40;
                    break;
                case 4:
                    cc = 60;
                    break;
                case 5:
                    cc = 80;
                    break;
            }
        }
        else if(prefWorld == 2)
        {
            switch (prefStage)
            {
                case 1:
                    cc = 100;
                    break;
                case 2:
                    cc = 120;
                    break;
                case 3:
                    cc = 140;
                    break;
                case 4:
                    cc = 160;
                    break;
                case 5:
                    cc = 180;
                    break;
            }
        }
        else if(prefWorld == 3)
        {
            switch (prefStage)
            {
                case 1:
                    cc = 200;
                    break;
                case 2:
                    cc = 220;
                    break;
                case 3:
                    cc = 240;
                    break;
                case 4:
                    cc = 260;
                    break;
                case 5:
                    cc = 280;
                    break;
            }
        }
        else if (prefWorld == 4)
        {
            switch (prefStage)
            {
                case 1:
                    cc = 300;
                    break;
                case 2:
                    cc = 320;
                    break;
                case 3:
                    cc = 340;
                    break;
                case 4:
                    cc = 360;
                    break;
                case 5:
                    cc = 380;
                    break;
            }
        }
        else if (prefWorld == 5)
        {
            switch (prefStage)
            {
                case 1:
                    cc = 400;
                    break;
                case 2:
                    cc = 420;
                    break;
                case 3:
                    cc = 440;
                    break;
                case 4:
                    cc = 460;
                    break;
                case 5:
                    cc = 480;
                    break;
            }
        }

        return cc;
        
    }


    // This function is to load game progress from the database and assigning the result to an AppData object
    void readData()
    {
        DataManager.getInstance().getGameData((result, msg) =>
        {
            if (result == null)
            {
                print("Get question failed from database!");
            }
            else
            {
                data = result;
                //test.charPoints = result.charPoints;
            }
        });
    }
}
