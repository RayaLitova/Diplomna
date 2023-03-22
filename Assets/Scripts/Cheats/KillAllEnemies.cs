using System.Collections;
using UnityEngine;

public class KillAllEnemies : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine("DestroyEnemies");
        }
    }

    IEnumerator DestroyEnemies()
    {
        for (int i = 0; i < GenerateDungeon.enemyCount; i++)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            yield return null; // to ensure destruction is finished before searching for next enemy
        }
        Destroy(this);
    }
}
