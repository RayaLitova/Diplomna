using UnityEngine;

public class KillBoss : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Destroy(GameObject.FindGameObjectWithTag("Boss"));
            Destroy(this);
        }
    }
}
