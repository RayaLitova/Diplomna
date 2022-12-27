using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : MonoBehaviour
{
    [SerializeField] public string description;
    public virtual void Action()
    {
        Debug.Log("Action called");
    }
}
