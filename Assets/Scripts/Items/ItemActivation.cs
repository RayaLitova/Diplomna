using UnityEngine;

public class ItemActivation : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    public static bool isCanvasDisabled = false; //for cutscenes
    private void OnEnable()
    {
        if (isCanvasDisabled) //cutscene
        {
            isCanvasDisabled = false;
            return;
        }
        GameObject.Find("Player").GetComponent<CharacterStats>().AddItem(GetComponent<DisplayItem>().GetItem());
    }

    public void PlayParticles()
    { 
        particles.SetActive(true);
    }

    private void OnDisable()
    {
        if (isCanvasDisabled) //cutscene
            return;

        GameObject.Find("Player").GetComponent<CharacterStats>().RemoveItem(GetComponent<DisplayItem>().GetItem());
    }
}
