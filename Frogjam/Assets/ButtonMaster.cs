using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonMaster : MonoBehaviour
{
    public ButtonPrefab CurrentButton;

    public GameObject Button1;
    public GameObject Button2;

    public void DisplayNextButtons()
    {
        Button1.SetActive(true);
        Button1.GetComponentInChildren<TextMeshProUGUI>().text = CurrentButton.NextButton.GetComponentInChildren<TextMeshProUGUI>().text;
        if(CurrentButton.SecondButton != null)
        {
            Button2.SetActive(true);
            Button2.GetComponentInChildren<TextMeshProUGUI>().text = CurrentButton.SecondButton.GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }

    public void ClickedButton1()
    {
        CurrentButton = Button1.GetComponent<ButtonPrefab>();
        Button1.SetActive(false);
        Button2.SetActive(false);
        // Call dialogue event
    }

    public void ClickedButton2()
    {
        CurrentButton = Button2.GetComponent<ButtonPrefab>();
        Button1.SetActive(false);
        Button2.SetActive(false);
        // Call dialogue event
    }
    
}
