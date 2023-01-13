using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour
{
    [SerializeField] float fadeoutTime = .001f;
    public enum AfterFadingAction { Destroy, SetInactive }
    public AfterFadingAction action;
    private Text text;

    void OnEnable()
    {
        text = GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        StartCoroutine("FadeOut");
    }
    IEnumerator FadeOut()
    {
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - fadeoutTime);
            yield return null;
        }
        switch (action)
        {
            case AfterFadingAction.Destroy:
                Destroy(gameObject);
                break;

            case AfterFadingAction.SetInactive:
                gameObject.SetActive(false);
                break;
        }
    }
}
