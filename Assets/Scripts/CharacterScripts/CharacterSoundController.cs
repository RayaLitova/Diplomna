using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterSoundController : MonoBehaviour
{
    [SerializeField] AudioSource characterAudio;
    public void PlayHitSound(int index)
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/Skills/" + UI_skillsManage.SkillAnimationIndex.FirstOrDefault(x => x.Value == index).Key);
        characterAudio.Play();
    }

    public void PlayDashSound()
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/Dash");
        characterAudio.Play();
    }

    public void PlayTakeDamageSound()
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/TakeDamage");
        characterAudio.Play();
    }

    public void PlayDeathSound()
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/Death");
        characterAudio.Play();
    }

    public void PlayBossSlainSound()
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/BossSlain/" + Random.Range(0, 3).ToString());
        characterAudio.Play();
    }

    public void PlaySafeRoomExitSound()
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/SafeRoomExit/" + Random.Range(0, 3).ToString());
        characterAudio.Play();
    }

    public void PlayEnterTavernSound()
    {
        characterAudio.clip = Resources.Load<AudioClip>("Audio/Character/TavernEnter/" + Random.Range(0, 3).ToString());
        characterAudio.Play();
    }

    public void FadeBackgrounMusic(AudioSource audioSource, float duration, float targetVolume)
    {
        StartCoroutine(SoundStaticFunctions.StartFade(audioSource, duration, targetVolume));
    }
}
