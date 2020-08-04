using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ApproveChallengeManager : MonoBehaviour {
    public Text[] t;
    private List<ChallengeQuestion> cqs;
    private ChallengeQuestion cq;
    private static Action<bool, string> callback;

    private void Start() {
        cqs = new List<ChallengeQuestion>();
        callback = (result, msg) => {
            Debug.Log("Approve Question: " + result + ", " + msg);
            if (result) {
                DisplayNextQuestion();
                // TODO: prompt add succeed
            } else {
                Debug.LogWarning("Approve Question failed from database!");
            }
        };

        
        GetQuestions();
    }

    // download questions from firebase
    private void GetQuestions() {
        FirebaseManager.getInstance().getQuestionList((result, msg) => {
            if (null == result) {
                Debug.LogError("Get question failed from database!");
                // TODO: go straight back to menu?
            } else {
                Debug.Log("Got questions from database!");
                foreach (ChallengeQuestion q in result) {
                    if (q.status == ChallengeQuestion.Status.Pending) {
                        cqs.Add(q);
                    }
                }

            }
            DisplayNextQuestion();
        });

    }

    // show the next question
    private void DisplayNextQuestion() {
        if (cqs.Count < 1) {
            DisplayEmpty();
            return;
        }
        cqs.RemoveAt(0);
        cq = cqs[0];

        t[0].text = cq.description;
        for (int i = 0; i < 4; i++) {
            t[1 + i].text = cq.choices[i];
        }
        // swap
        string tmp = t[1].text;
        t[1].text = t[1 + cq.idxAnswer].text;
        t[1 + cq.idxAnswer].text = tmp;
    }

    // when no more questions to approve
    private void DisplayEmpty() {
        cq = null;
        t[0].text = "No available questions pending approval!";
        t[1].text = "";
        t[2].text = "";
        t[3].text = "";
        t[4].text = "";
    }

    // UI button actions
    public void Approve() {
        if (cq == null) return;
        FirebaseManager.getInstance().doApproveQuestion(cq, true, callback);

    }
    public void Reject() {
        if (cq == null) return;
        FirebaseManager.getInstance().doApproveQuestion(cq, false, callback);
    }

}
