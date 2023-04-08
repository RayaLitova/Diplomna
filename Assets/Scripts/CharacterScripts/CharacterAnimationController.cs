using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animator animationController;
    CharacterSoundController soundController;

    private void Start()
    {
        animationController = GetComponent<Animator>();
        soundController = GetComponent<CharacterSoundController>();
    }
    public void SetDash(bool isActive)
    {
        animationController.SetBool("Dash", isActive);
        if (isActive)
            soundController.PlayDashSound();
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

    public void SetTakeDamage(bool isActive)
    {
        animationController.SetBool("DamageTaken", isActive);
        if (isActive)
            soundController.PlayTakeDamageSound();
    }

    public void SetIsDead(bool isActive)
    {
        animationController.SetBool("isDead", isActive);
        if (isActive)
            soundController.PlayDeathSound();
    }

    public void SetHit(bool isActive, float index = 0f)
    { 
        animationController.SetBool("Hit", isActive);
        animationController.SetFloat("SpellIndex", index);
        if(isActive)
            soundController.PlayHitSound((int)(index * UI_skillsManage.SkillAnimationCount));
    }

    public void Gather(bool isActive)
    {
        animationController.SetBool("isGathering", isActive);
    }
}
