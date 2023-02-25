using UnityEngine;

public class OnPlayerTriggerDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Destroy(gameObject.transform.parent.gameObject);
    }
}
