using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GroupChallengeManager : MonoBehaviour {
    public Text[] t;
    private List<ChallengeQuestion> cqs = new List<ChallengeQuestion>();

    private int qcount = 0;

    private void Start() {
        GetQuestions();
    }

    // fetch all questions that are already approved to group into a challenge
    private void GetQuestions() {
        FirebaseManager.getInstance().getQuestionList((result, msg) => {
            if (null == result) {
                Debug.LogError("Get question failed from database!");
                // TODO: go straight back to menu?
            } else {
                Debug.Log("Got questions from database!");
                foreach (ChallengeQuestion q in result) {
                    if (q.status == ChallengeQuestion.Status.Approved) {
                        cqs.Add(q);
                        qcount++;
                    }
                }
                t[0].text = "" + qcount;
            }
        });

    }

    // UI action of Publishing a challenge, modify all questions involved
    public void Publish() {
        if (qcount < 1) {
            Debug.LogWarning("No question to publish!");
            return;
        }
        FirebaseManager.getInstance().doPublishChallenge(cqs,
            (result, msg) => {
                Debug.Log("Publish challenge: " + result + ", " + msg);
                if (result) {
                    Debug.Log("Published!");
                    qcount = 0;
                    t[0].text = "" + qcount;
                } else {
                    Debug.LogWarning("Publish challenge failed from database!");
                }
            }
        );

    }

}
