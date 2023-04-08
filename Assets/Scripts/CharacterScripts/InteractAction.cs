using UnityEngine;

public abstract class InteractAction : MonoBehaviour
{
    [SerializeField] public string description;
    [SerializeField] public AudioClip audioClip = null;
    public bool interactAvailable = false;
    public abstract void Action();
}
