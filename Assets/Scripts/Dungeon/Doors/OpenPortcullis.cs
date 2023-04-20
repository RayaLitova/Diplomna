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
        if (targetRoom.EnemiesInRoom())
            return;

        animator.SetBool("open", true);
    }
}
