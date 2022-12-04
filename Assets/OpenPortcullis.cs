using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPortcullis : MonoBehaviour
{
    [SerializeField] CheckRoom targetRoom;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (targetRoom.GetNumberOfTags("Enemy") == 0)
        {
            Debug.Log("Open door " + gameObject.name);
            animator.SetBool("open", true);
        }

    }
}
