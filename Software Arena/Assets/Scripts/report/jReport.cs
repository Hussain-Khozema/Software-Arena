using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to define a report. values here represent the elements that make up a report.
//date = date the report was created
//time = time the report was created
//nss... = "no. of students" currently in that "world no." where "..." is the "world no." 
//nas... = "avg. no. of attempts" for all students in that "world no." where "..." is the "world no."
//nacm... = avg. no of challenge questions answered correctly 

[System.Serializable]
public class jReport
{
    public string date,time;
    public int nss1 = 0, nss2 = 0, nss3 = 0, nss4 = 0, nss5 = 0;
    public double nas1 = 0, nas2 = 0, nas3 = 0, nas4 = 0, nas5 = 0, nacm = 0;
}
