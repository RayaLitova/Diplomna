using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFadeOut : MonoBehaviour
{

    void OnEnable()
    {
        StartCoroutine("FadeOut");
    }
    IEnumerator FadeOut()
    { 
        Text floor_text1 = transform.GetChild(0).GetComponent<Text>();
        Text floor_text2 = transform.GetChild(1).GetComponent<Text>();
        
        while (floor_text1.color.a > 0.0f)
        {
            floor_text1.color = new Color(floor_text1.color.r, floor_text1.color.g, floor_text1.color.b, floor_text1.color.a - .001f);
            floor_text2.color = new Color(floor_text2.color.r, floor_text2.color.g, floor_text2.color.b, floor_text2.color.a - .001f);
            yield return null;
        }
        Destroy(gameObject);
    }
}
