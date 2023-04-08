using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnterTavernSound : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Player").GetComponent<CharacterSoundController>().PlayEnterTavernSound();
    }
}
