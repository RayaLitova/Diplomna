using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    public static PortalActivationCutscene teleport;

    private void Start()
    {
        //teleport = GameObject.Find("Teleporter").GetComponent<PortalActivationCutscene>();
    }
    private void OnDestroy()
    {
        teleport.StartCutscene();
    }
}
