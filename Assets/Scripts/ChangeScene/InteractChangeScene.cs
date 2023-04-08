using UnityEngine;

public class InteractChangeScene : InteractAction
{
    [SerializeField] string sceneName;
    bool soundPlayed = false;
    public override void Action()
    {
        LoadScene.Load(sceneName);
    }

    private void Update()
    {
        if (audioClip == null || soundPlayed)
            return;
        if (!interactAvailable)
        {
            soundPlayed = false;
            return;
        }
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
        soundPlayed = true;
    }
}
