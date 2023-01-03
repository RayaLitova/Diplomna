using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHide_InteractionUI : MonoBehaviour
{
    GameObject line;
    GameObject text1;

    public static string text = "";

    private void Start()
    {
        line = transform.GetChild(0).gameObject;
        text1 = transform.GetChild(1).gameObject;
    }
    public void Show()
    {
        line.SetActive(true); 
    }

    public void Hide()
    {
        line.SetActive(false);
        text1.SetActive(false);

    }

    public void SetText()
    {
        if (!line.activeSelf)
            Show();
        text1.SetActive(false);
        text1.SetActive(true);
        text1.GetComponent<Text>().text = text + " [F]";
    }

    private void Update()
    {
        if (text != "")
        {
            SetText();
            text = "";
        }
        if (!line.activeSelf)
            return;

        if (!text1.activeSelf)
            line.SetActive(false);
    }
}

