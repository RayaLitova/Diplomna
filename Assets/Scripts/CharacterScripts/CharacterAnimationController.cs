using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animator animationController;

    private void Start()
    {
        animationController = GetComponent<Animator>();
    }
    public void SetDash(bool isActive)
    {
        animationController.SetBool("Dash", isActive);
    }

    public bool GetIsInCombat()
    {
        return animationController.GetBool("isInCombat");
    }

    public void SetIsInCombat(bool isActive)
    {
        animationController.SetBool("isInCombat", isActive);
    }

    public void SetMove(bool isActive)
    {
        animationController.SetBool("isMoving", isActive);
        animationController.SetFloat("MoveSpeed", GetIsInCombat() ? 1 : 0);
    }

    public AnimatorStateInfo GetCurrentAnimatorStateInfo()
    {
        return animationController.GetCurrentAnimatorStateInfo(0);
    }
}
