using UnityEngine;

[CreateAssetMenu(fileName = "New Executable", menuName = "Executable")]
public class Executable : Usable
{
    public float cooldown = 0f;

    public float cooldownTimer = 0f;
    public bool isCDRapplied = false;
    public Usables_UI_manager ui_manager;
    public virtual void Execute() { }
    public void ApplyTBSCooldown() 
    {
        if (cooldownTimer < ui_manager.timeBetweenExecutes + Time.time)
            cooldownTimer = Time.time + ui_manager.timeBetweenExecutes;
    }

    public void ApplyCooldown()
    { 
        cooldownTimer = Time.time + cooldown;
    }

    public void ReduceCooldown(float cooldownReduction)
    {
        if (isCDRapplied)
            return;

        cooldown -= cooldownReduction;
        isCDRapplied = true;
    }
}
