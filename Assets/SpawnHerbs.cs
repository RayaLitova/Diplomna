using UnityEngine;

public class SpawnHerbs : MonoBehaviour
{
    void Start()
    {
        var herbs = Resources.LoadAll<GameObject>("Herbs");
        int herbCount = Random.Range(0, 5);
        GenerateDungeon.herbCount += herbCount;
        GameObject.Find("Scripts").GetComponent<DungeonObjectives>().UpdateHerbTargetCount();
        for (int i = 0; i < herbCount; i++)
        {
            Transform pos;
            do
            {
                pos = transform.GetChild(i + Random.Range(0, transform.childCount - (i + 1)));
            } while (pos.childCount != 0);

            Instantiate(herbs[Random.Range(0, herbs.Length)], pos);
        }
    }
}
