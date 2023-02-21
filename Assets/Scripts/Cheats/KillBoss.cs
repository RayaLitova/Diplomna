using UnityEngine;

public class KillBoss : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Destroy(GameObject.FindGameObjectWithTag("Boss"));
            Destroy(this);
        }
    }
}
