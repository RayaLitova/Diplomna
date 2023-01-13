using UnityEngine;
using UnityEngine.UI;

public class ShowHide_InteractionUI : MonoBehaviour
{
    GameObject line;
    GameObject text;

    public static string textString = "";

    private void Start()
    {
        line = transform.GetChild(0).gameObject;
        text = transform.GetChild(1).gameObject;
    }
    public void Show()
    {
        line.SetActive(true); 
    }

    public void Hide()
    {
        line.SetActive(false);
        text.SetActive(false);

    }

    public void SetText()
    {
        if (!line.activeSelf)
            Show();
        text.SetActive(false);
        text.SetActive(true);
        text.GetComponent<Text>().text = textString + " [F]";
    }

    private void Update()
    {
        if (textString != "")
        {
            SetText();
            textString = "";
        }
        if (!line.activeSelf)
            return;

        if (!text.activeSelf)
            line.SetActive(false);
    }
}

