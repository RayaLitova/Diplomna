using UnityEngine;

public class SoundStartFadeIn : MonoBehaviour
{
    void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        StartCoroutine(SoundStaticFunctions.StartFade(source, 3f, 0.5f));
    }
}
