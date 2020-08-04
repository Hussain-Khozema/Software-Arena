using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewReportPopup : MonoBehaviour
{
    public GameObject popupconfirm,button1,button2;

    public void Popup() {
        popupconfirm.SetActive(true);
    }
    public void clickedYes(int SceneIndex)
    {
        popupconfirm.SetActive(false);
        SceneManager.LoadScene(SceneIndex);
    }

    public void clickedNo() {
        popupconfirm.SetActive(false);
    }
}
