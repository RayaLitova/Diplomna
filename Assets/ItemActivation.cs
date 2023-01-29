using UnityEngine;

public class ItemActivation : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<CharacterStats>().AddItem(GetComponent<DisplayItem>().GetItem());
    }

    public void PlayParticles()
    { 
        particles.SetActive(true);
    }

    private void OnDisable()
    {
        GameObject.Find("Player").GetComponent<CharacterStats>().RemoveItem(GetComponent<DisplayItem>().GetItem());
        GetComponent<DisplayItem>().Remove();
    }
}
