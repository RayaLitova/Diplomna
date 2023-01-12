using UnityEngine;

public abstract class InteractAction : MonoBehaviour
{
    [SerializeField] public string description;
    public abstract void Action();
}
