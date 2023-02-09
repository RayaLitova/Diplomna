using UnityEngine;

public class LinkDisable : MonoBehaviour //Disable object if linked object is disabled
{
    [SerializeField] GameObject linked;
    void Update()
    {
        if (linked.activeInHierarchy)
            return;
        ItemActivation.isCanvasDisabled = true;
        gameObject.SetActive(false);
    }
}
