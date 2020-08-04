using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class QuestionList
{
    public List<Questions> jRecords = new List<Questions>();

}

public class ChallengeApproveController : MonoBehaviour
{

    //public jRecordsList jRecordList = new jRecordsList();

    void Start()
    {

        TextAsset asset = Resources.Load("jRecords") as TextAsset;
   
        if (asset != null)
        {                                                
            //jRecordList = JsonUtility.FromJson<jRecordsList>(asset.text);
            //foreach (jRecords record in jRecordList.jRecords) {

            //    //print(record.date);                   //for debugging

            //    GameObject button = Instantiate(buttonTemplate) as GameObject;
            //    button.SetActive(true);

            //    button.GetComponent<rlButtonListButton>().SetText(record.date);
            //    button.GetComponent<Button>().onClick.AddListener(delegate { ChangeScene(record); });
            //    button.transform.SetParent(buttonTemplate.transform.parent, false);
            //}
        }
        else {
            print("Asset is null");
        }

    }
}
