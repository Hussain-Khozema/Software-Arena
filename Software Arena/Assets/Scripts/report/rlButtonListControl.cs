using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class rlButtonListControl : MonoBehaviour
{
    //used to generate list of buttons for each report.
    [SerializeField]
    public GameObject buttonTemplate;

    //obj used to reference reportDetails.unity.
    //empty used to reference message when there is no information to be retrieved
    public GameObject obj,empty;

    //variables used to produce the report. for more details, please refer to jReport.cs
    public int nss1 = 0, nss2 = 0, nss3 = 0, nss4 = 0, nss5 = 0;
    public double nas1 = 0,nas2 = 0, nas3 = 0, nas4 = 0, nas5 = 0, nacm = 0;

    //used to check if a report is already generated on the same day.
    public static bool exists;

    //datetime of system.
    DateTime date;

    //datetime used by init report.
    DateTime init=new DateTime();

    

    void Start()
    {

        //create initial report to create root in database. will be removed when reports are retrieved.
        jReport tmp = new jReport();
        tmp.date=init.ToString("dd/MM/yyyy");
        savejReport(tmp);

        //get the date of the system now.
        date = System.DateTime.Now;
        Debug.Log("date is = " + date);

        //get all reports in the database if any.
        FirebaseManager.getInstance().getAllReport((result, msg) =>
        {
            Debug.Log("getAllReport, operation : " + msg);
            if (result.Count >= 1)
            {
                //list is not empty. remove empty message.
                empty.SetActive(false);

                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    foreach (jReport t_report in result)
                    {
                        if ((t_report.date.Equals(date.ToString("dd/MM/yyyy"))))
                        {
                            //if sunday, check all reports in database.
                            //if a report in database has the same date as today, set exists flag to true;
                            exists = true;
                            break;
                        }
                        else
                            exists = false;
                    }

                    //if report does not exist in database, generate the report.
                    Debug.Log("start, exists = " + exists);
                    if (exists == false)
                        getData();

                    //if report exists in database, just generate the report.
                    else
                        generateReports();

                }
                else { 
                    //when its not a sunday, just generate a report
                    generateReports();
                }
            }
            else {

                //if returned list is empty...
                Debug.Log("result count : " + result.Count);
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    getData();
                    empty.SetActive(false);
                }
                else {
                    empty.SetActive(true);
                }
                
            }

        });

    }

    //method to get GameData from database
    public void getData()
    {
        //get all game data in database.
        List<AppData> AppDataList = new List<AppData>();

        FirebaseManager.getInstance().getAllGameData((result, msg) =>
        {
            if (result != null)
            {
                AppDataList = result;
                Debug.Log("getData, appDataList.Count = " + AppDataList.Count);
                createReport(AppDataList);
            }
            Debug.Log(msg);
        });
    }

    //method to create a report based on GameData retrieved
    public void createReport(List<AppData> AppDataList)
    {

        //after retrieving the data, need to get neccessary perimeters.
        Debug.Log("createReports, appDataList.Count = " + AppDataList.Count);
        if (AppDataList.Count >= 1)
        {
            foreach (AppData data in AppDataList)
            {
                switch (data.progress.curWorld)
                {
                    case 1:
                        nss1++;
                        nas1 += data.progress.avgAttempts;
                        Debug.Log("nss1 = " + nss1);
                        Debug.Log("nas1 = " + nas1);
                        break;
                    case 2:
                        nss2++;
                        nas1++;
                        nas2 += data.progress.avgAttempts;
                        Debug.Log("nss2 = " + nss2);
                        Debug.Log("nas2 = " + nas2);
                        break;
                    case 3:
                        nss3++;
                        nas2++;
                        nas1++;
                        nas3 += data.progress.avgAttempts;
                        Debug.Log("nss3 = " + nss3);
                        Debug.Log("nas3 = " + nas3);
                        break;
                    case 4:
                        nss4++;
                        nas3++;
                        nas2++;
                        nas1++;
                        nas4 += data.progress.avgAttempts;
                        Debug.Log("nss4 = " + nss4);
                        Debug.Log("nas4 = " + nas4);
                        break;
                    case 5:
                        nss5++;
                        nas4++;
                        nas3++;
                        nas2++;
                        nas1++;
                        nas5 += data.progress.avgAttempts;
                        Debug.Log("nss5 = " + nss5);
                        Debug.Log("nas5 = " + nas5);
                        break;
                }
                nacm += data.challengePoints;
            }
            nacm /= (10 * AppDataList.Count);
            nas1 /= AppDataList.Count;
            nas2 /= AppDataList.Count;
            nas3 /= AppDataList.Count;
            nas4 /= AppDataList.Count;
            nas5 /= AppDataList.Count;


            jReport jreport = new jReport();
            jreport.date = date.ToString("dd/MM/yyyy");
            jreport.time = date.ToString("h:mm:ss tt");
            jreport.nss1 = nss1;
            jreport.nss2 = nss2;
            jreport.nss3 = nss3;
            jreport.nss4 = nss4;
            jreport.nss5 = nss5;
            jreport.nas1 = nas1;
            jreport.nas2 = nas2;
            jreport.nas3 = nas3;
            jreport.nas4 = nas4;
            jreport.nas5 = nas5;
            jreport.nacm = nacm;
            savejReport(jreport);

            generateReports();
        }
        else
        {
            print("no data");
        }
    }

    //method to save created report to database
    public void savejReport(jReport report)
    {
        //save generated report to the database
        FirebaseManager.getInstance().saveReport(report, (result, msg) =>
        {
            Debug.Log(msg);
        });
    }

    //method to generate buttons from the retrieved reports
    public void generateReports()
    {
        //recall all existing reports in firebase
        List<jReport> jReportList = new List<jReport>();
        FirebaseManager.getInstance().getAllReport((result, msg) =>
        {
            Debug.Log("getAllReport, operation : " + msg);
            if (result != null)
            {
                jReportList = result;

                //create buttons for each report
                foreach (jReport report in jReportList)
                {
                    GameObject button = Instantiate(buttonTemplate) as GameObject;
                    button.SetActive(true);

                    button.GetComponent<rlButtonListButton>().SetText(report.date + " " + report.time);
                    button.GetComponent<Button>().onClick.AddListener(delegate { ChangeScene(report); });
                    button.transform.SetParent(buttonTemplate.transform.parent, false);
                }
            }

        });

        //used variables are reset.
        nss1 = 0;
        nss2 = 0;
        nss3 = 0;
        nss4 = 0;
        nss5 = 0;
        nas1 = 0;
        nas2 = 0;
        nas3 = 0;
        nas4 = 0;
        nas5 = 0;
        nacm = 0;
    }

    //method to change to reportDetails page
    public void ChangeScene(jReport report)
    {
        Debug.Log("changescene 1, exists = " + exists);
        reportDetails rd = obj.GetComponent<reportDetails>();

        //modify the text in reportDetails.unity with relevant information.
        rd.ModText(report);

        //change to reportDetails.unity page.
        SceneManager.LoadScene(22);

        //reset exists variable.
        exists = rd.ReturnExists(exists);

        Debug.Log("changescene 2, exists = " + exists);
    }

}

