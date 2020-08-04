using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//template used to generate reportDetails.unity, showing the details of each report.
public class reportDetails : MonoBehaviour
{
    public Text date,time,nss1,nss2,nss3,nss4,nss5,nas1,nas2,nas3,nas4,nas5,nacm;

    //modify the text in the page to details in the report retrieved.
    public void ModText(jReport report)
    {
        date.text = report.date;
        time.text = report.time;

        nss1.text = report.nss1.ToString();
        nss2.text = report.nss2.ToString();
        nss3.text = report.nss3.ToString();
        nss4.text = report.nss4.ToString();
        nss5.text = report.nss5.ToString();
        nas1.text = report.nas1.ToString();
        nas2.text = report.nas2.ToString();
        nas3.text = report.nas3.ToString();
        nas4.text = report.nas4.ToString();
        nas5.text = report.nas5.ToString();
        nacm.text = report.nacm.ToString();

    }

    //method to go back to reportList.unity when back button pressed.
    public void ReturnToArchive()
    {
        
        SceneManager.LoadScene("reportList");
        
    }

    //reset exists value from rlButtonListControl.cs
    public bool ReturnExists(bool value)
    {
        value = false;
        return value;
    }

}


