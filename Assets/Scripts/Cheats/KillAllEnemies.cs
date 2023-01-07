using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(GameObject.FindGameObjectWithTag("Enemy"));
        for (int i = 0; i < SpawnEnemies.enemyCount; i++)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            yield return null; // to ensure destruction is finished before searching for next enemy
        }
        Destroy(this);
    }
}
