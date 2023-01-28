using UnityEngine;

public class OpenCloseMenu : MonoBehaviour
{
    public KeyCode keyCode;
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
            ChangeMenuVisibility();
    }

    public void ChangeMenuVisibility()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
    }
}
