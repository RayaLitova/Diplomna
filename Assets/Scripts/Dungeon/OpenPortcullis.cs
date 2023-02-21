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
        if (targetRoom.GetNumberOfTags("Enemy") != 0)
            return;

        animator.SetBool("open", true);
    }
}
