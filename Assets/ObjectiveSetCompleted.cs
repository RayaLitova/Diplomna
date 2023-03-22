using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSetCompleted : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void SetCompleted()
    {
        DungeonObjectives.completedObjectivesCount++;
        for (int i = 0; i < 4; i++)
        { 
            text.text += transform.GetChild(i).GetComponent<Text>().text;
            text.text += " ";
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
