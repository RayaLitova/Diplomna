using UnityEngine;

public class OnPlayerTriggerDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterSoundController>().PlaySafeRoomExitSound();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
