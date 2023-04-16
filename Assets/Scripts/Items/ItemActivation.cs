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
        GameObject.Find("Player").GetComponent<CharacterStats>().AddItem((Item)(GetComponent<DisplayItem>().Get()));
    }

    public void PlayParticles()
    { 
        particles.SetActive(true);
    }

    private void OnDisable()
    {
        if (isCanvasDisabled) //cutscene
            return;

        GameObject.Find("Player").GetComponent<CharacterStats>().RemoveItem((Item)GetComponent<DisplayItem>().Get());
    }
}
