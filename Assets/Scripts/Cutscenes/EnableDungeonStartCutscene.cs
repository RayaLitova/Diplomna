using UnityEngine;

public class EnableDungeonStartCutscene : MonoBehaviour
{
    void OnEnable()
    {
        GameObject.Find("CinematicCamera").GetComponent<MoveTowardsBoss>().enabled = true;
    } 
}
