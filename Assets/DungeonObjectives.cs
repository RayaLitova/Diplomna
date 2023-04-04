using UnityEngine;
using UnityEngine.UI;

public class DungeonObjectives : MonoBehaviour
{
    private int targetEnemyCount;
    private int currEnemyCount = 0;
    private int targetHerbCount;
    private int currHerbCount = 0;
    public static int objectivesCount = 1;
    public static int completedObjectivesCount = 0;
    [SerializeField] Text targetCount1;
    [SerializeField] Text currProgress1;
    [SerializeField] Text targetCount2;
    [SerializeField] Text currProgress2;

    public void UpdateTargetCount()
    {
        targetEnemyCount = Random.Range(GenerateDungeon.enemyCount / 2, GenerateDungeon.enemyCount + 1);
        targetCount1.text = targetEnemyCount.ToString();
    }

    public void UpdateHerbTargetCount()
    {
        targetHerbCount = Random.Range(GenerateDungeon.herbCount / 2, GenerateDungeon.herbCount + 1);
        targetCount2.text = targetHerbCount.ToString();
    }

    public void UpdateCurrProgress()
    {
        currEnemyCount++;
        currProgress1.text = currEnemyCount.ToString();
        if (currEnemyCount == targetEnemyCount)
            GameObject.Find("Objective1").GetComponent<ObjectiveSetCompleted>().SetCompleted();
            
    }

    public void UpdateHerbProgress()
    {
        currHerbCount++;
        currProgress2.text = currHerbCount.ToString();
        if(currHerbCount == targetHerbCount)
            GameObject.Find("Objective2").GetComponent<ObjectiveSetCompleted>().SetCompleted();
    }
}
