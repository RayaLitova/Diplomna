using UnityEngine;
using UnityEngine.UI;

public class RandomBackgroundImage : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("LoadingScreenBG/1"); 
    }
}
