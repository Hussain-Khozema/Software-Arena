using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberMe : MonoBehaviour
{
    public Button button;
    public Sprite check_off;
    public Sprite check_on;

    // Initially the remember me box is checked off
    void Start()
    {
        button.image.sprite = check_off;
    }

    // This function changes the remember me box when clicked
    public void Changeimage()
    {
        if (button.image.sprite == check_off)
        {
            button.image.sprite = check_on;
        } else
        {
            button.image.sprite = check_off;
        }
    }
}
