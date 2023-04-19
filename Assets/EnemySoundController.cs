using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private string enemyType;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalkingSound()
    {
        audioSource.loop = true;
        audioSource.clip = Resources.Load<AudioClip>("Audio/Enemies/" + enemyType + "/Walking");
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.loop = false;
        audioSource.clip = Resources.Load<AudioClip>("Audio/Enemies/" + enemyType + "/Attack");
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        audioSource.loop = false;
        audioSource.clip = Resources.Load<AudioClip>("Audio/Enemies/" + enemyType + "/Death");
        audioSource.Play();
    }

    public void PlayTakeDamageSound()
    {
        audioSource.loop = false;
        audioSource.clip = Resources.Load<AudioClip>("Audio/Enemies/" + enemyType + "/TakeDamage");
        audioSource.Play();
    }
}
