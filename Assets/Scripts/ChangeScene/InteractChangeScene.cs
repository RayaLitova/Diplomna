using UnityEngine;

public class InteractChangeScene : InteractAction
{
    [SerializeField] string sceneName;
    public override void Action()
    {
        LoadScene.Load(sceneName);
    }
}
