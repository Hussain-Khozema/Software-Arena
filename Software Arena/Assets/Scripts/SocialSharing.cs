using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class SocialSharing : MonoBehaviour
{
    public string m_shareMessage;
    public string m_shareURL;

    void FinishedSharing(eShareResult _result)
    {
        Debug.Log("Finished sharing");
        Debug.Log("Share Result = " + _result);
    }
    public void SocialShare()
    {
        // Create share sheet
        SocialShareSheet _shareSheet = new SocialShareSheet();
        _shareSheet.Text = m_shareMessage;

        // Add below line if you want to share URL
        _shareSheet.URL = m_shareURL;

        // Add below line if you want to share a screenshot
        _shareSheet.AttachScreenShot();


        // Show composer
        NPBinding.UI.SetPopoverPointAtLastTouchPosition(); // To show popover at last touch point on iOS. On Android, its ignored.
        NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
    }
}
