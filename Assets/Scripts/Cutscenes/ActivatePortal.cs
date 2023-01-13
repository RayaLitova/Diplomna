using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    [SerializeField] PortalActivationCutscene teleport;
    private void OnDestroy()
    {
        teleport.StartCutscene();
    }
}
