using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class CLeaderBoard : MonoBehaviour {
    public Text highscore;
    public Text playername;
    public Text rank;
    public Text title;
    private Transform container;
    private Transform template;
    public GameObject lining;
    public GameObject crown;

    private List<LeaderboardRecord> datas = new List<LeaderboardRecord>();
    private List<Transform> displayingRows = new List<Transform>();
    private bool showingChallenge = true;

    void Start() {

        showingChallenge = true;

        container = GameObject.Find("LeaderboardContainer").transform;       //locating the leaderboard container
        //template = transform.Find("LeaderboardContainer").Find("LeaderboardTemplate");
        template = GameObject.Find("LeaderboardTemplate").transform;         //locating the leaderboard template

        //Gamedata user1 = new Gamedata("loser1", 52, 78);
        //Gamedata user2 = new Gamedata("loser2", 74, 25);
        //Gamedata user3 = new Gamedata("loser3", 35, 99);
        //Gamedata user4 = new Gamedata("loser4", 17, 68);
        //Gamedata user5 = new Gamedata("loser5", 88, 43);
        //Gamedata user6 = new Gamedata("loser6", 25, 80);
        //Gamedata user7 = new Gamedata("loser7", 90, 52);
        //Gamedata user8 = new Gamedata("loser8", 79, 17);
        //datas = new List<Gamedata>() { user1, user2, user3, user4, user5, user6, user7, user8 };

        List<AppData> apds = new List<AppData>();                            //get the list of rank
        FirebaseManager.getInstance().getAllGameData((result, msg) => {      //to test if any data is retrieved
            Debug.Log("Getting all appdatas: " + msg);
            if (result != null) {
                foreach (AppData apd in result) {
                    datas.Add(new LeaderboardRecord(apd.name, apd.challengePoints, apd.multiPlayerPoints));
                }
                ChallengeRank();
            } else {
                Debug.LogWarning("Get game data failed from database!");
            }
        });



    }

    // Function to display challenge mode rank after sorting the data
    public void ChallengeRank() {                                        

        title.text = "Challenge Mode";

        datas.Sort(delegate (LeaderboardRecord x, LeaderboardRecord y) {       //sorting the list

            return y.challenge_points.CompareTo(x.challenge_points);

        });

        Display();                                                  //display rank
        //template.gameObject.SetActive(false);
        //lining.gameObject.SetActive(false);
        //crown.gameObject.SetActive(false);


        //Transform duplicate2 = Instantiate(template, container);
        //duplicate2.gameObject.SetActive(true);
        //duplicate2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20 * datas.Count);
        //lining.gameObject.SetActive(true);

    }

    // Function to display multiplayer mode rank after sorting the data
    public void MultiplayerRank() {                                        

        title.text = "Multiplayer Mode";

        datas.Sort(delegate (LeaderboardRecord x, LeaderboardRecord y) {        //sorting the list
            return y.multiplayer_points.CompareTo(x.multiplayer_points);
        });

        Display();                                                          //display rank

    }

    // The template for displaying the ranks either in multiplayer or challenge mode
    // The ranks will either be sorted by multiplayer points or challenge points
    // displaying top 10
    private void Display() {                                              

        ClearBoard();                                            //clear the board to show either multiplayer/challenge mode rank
        template.gameObject.SetActive(true);
        for (int i = 0; i < 10; i++) {
            if (i >= datas.Count) { break; }

            Transform duplicate = Instantiate(template, container);       //instantiating the template and the container
            displayingRows.Add(duplicate);
            duplicate.gameObject.SetActive(true);
            duplicate.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20 * i);
            duplicate.Find("Rank Number").GetComponent<Text>().text = "" + (i + 1);
            if (showingChallenge) {                                 //show rank either based on multiplayer/challenge points/rank
                duplicate.Find("Player Scores").GetComponent<Text>().text = "" + datas[i].challenge_points;
            } else {
                duplicate.Find("Player Scores").GetComponent<Text>().text = "" + datas[i].multiplayer_points;
}
            duplicate.Find("Player Names").GetComponent<Text>().text = datas[i].student_uid;
            if (i != 0) {
                duplicate.Find("Crown").gameObject.SetActive(false);
            }

        }
        template.gameObject.SetActive(false);
    }


    // Switching between displaying multiplayer/challenge mode rank
    public void Switch() {                                      
        showingChallenge = !showingChallenge;
        if (showingChallenge) {
            ChallengeRank();
        } else {
            MultiplayerRank();
        }

        
    }

    // Clear the leaderboard UI so that data will not overlap between transitions
    void ClearBoard() {                                  
        Debug.Log(displayingRows.Count);
        foreach (Transform t in displayingRows) {
            Destroy(t.gameObject);
        }
        displayingRows.Clear();
    }

    [System.Serializable]
    public class LeaderboardRecord {

        public string student_uid;        // userid (username)
        public int challenge_points;      // no. of matches won, 1 point per match
        public int multiplayer_points;
        public LeaderboardRecord(string student_uid, int challenge_points, int multiplayer_points) {

            this.student_uid = student_uid;
            this.challenge_points = challenge_points;
            this.multiplayer_points = multiplayer_points;
        }
    }
}
