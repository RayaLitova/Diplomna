using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnRandomPeriods : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("PlaySound");
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(Random.Range(5, 20));
        }
    }
}
