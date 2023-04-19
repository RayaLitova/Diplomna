using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private float animationTimer;
    private CharacterStats stats;
    private EnemyAnimationController animationController;
    private EnemyAttack enemyAttack;
    private void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        stats = GetComponent<CharacterStats>();
        animationController = GetComponent<EnemyAnimationController>();
        animationTimer = Time.time + 2f;
    }

    void Update()
    {
        if (Time.time < animationTimer)
            return;
        enemyAttack.isAttackingDisabled = false;
        animationController.takeDamageAnimation(false);
    }
    public void TakeDamage(CharacterStats playerStats)
    {
        enemyAttack.isAttackingDisabled = true;

        animationTimer = Time.fixedTime + 1.0f;
        animationController.takeDamageAnimation(true);
        int damage = playerStats.CalcDamageAgainst(stats, UI_skillsManage.GetCurrentSkillInfo());
        stats.Health -= damage;
        ShowDamagePopups.ShowPopup(playerStats.DamagePopupType, damage, transform.position);
        if (stats.Health <= 0)
        {
            Destroy(enemyAttack.particles.gameObject);
            animationController.deathAnimation(true);
        }
        UI_skillsManage.FinishSkillExecution();
    }

    private void OnDestroy()
    {
        GameObject.Find("Scripts").GetComponent<DungeonObjectives>().UpdateCurrProgress();
    }

}
