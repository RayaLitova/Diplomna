using UnityEngine;
using UnityEngine.UI;

public class DungeonObjectives : MonoBehaviour
{
    private int targetEnemyCount;
    private int currEnemyCount = 0;
    public static int objectivesCount = 1;
    public static int completedObjectivesCount = 0;
    [SerializeField] Text targetCount;
    [SerializeField] Text currProgress;
    public void UpdateTargetCount()
    {
        targetEnemyCount = Random.Range(GenerateDungeon.enemyCount / 2, GenerateDungeon.enemyCount + 1);
        targetCount.text = targetEnemyCount.ToString();
    }

    public void UpdateCurrProgress()
    {
        currEnemyCount++;
        currProgress.text = currEnemyCount.ToString();
        if (currEnemyCount == targetEnemyCount)
            GameObject.Find("Objective1").GetComponent<ObjectiveSetCompleted>().SetCompleted();
            
    }
}
