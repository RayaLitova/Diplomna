using System.Collections;
using UnityEngine;

public class PortalActivationCutscene : MonoBehaviour
{
    [SerializeField] GameObject portalActive;
    [SerializeField] GameObject portalUnactive;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject cinematicCamera;

    private Renderer portalUnactiveRenderer;
    private Color portalUnactiveColor;
    private GameObject mainCamera;
    private void Start()
    {
        cinematicCamera = GameObject.Find("CinematicCamera");
        portalUnactiveRenderer = portalUnactive.GetComponent<Renderer>();
        portalUnactiveColor = portalUnactiveRenderer.material.color;
    }
    public void StartCutscene()
    {
        mainCamera = Camera.main.gameObject;
        mainCamera.SetActive(false);
        cinematicCamera.SetActive(true);
        cinematicCamera.transform.position = portalActive.transform.position;
        //Move to see the portal from the right position
        cinematicCamera.transform.position -= portalActive.transform.forward * 100f;

        //Rotate to see the portal from the right angle
        cinematicCamera.transform.eulerAngles = cinematicCamera.transform.eulerAngles + LoadDungeon.cinematicCameraRotation; 

        portalActive.SetActive(true);
        StartCoroutine("FadeOut");

    }

    IEnumerator FadeOut() // Portal activation cutscene
    {
        while (portalUnactiveRenderer.material.color.a > 0f)
        {
            portalUnactiveRenderer.material.color = new Color(portalUnactiveColor.r, portalUnactiveColor.g,
                portalUnactiveColor.b, portalUnactiveColor.a - 0.01f);
            portalUnactiveColor.a -= 0.01f;
            if(portalUnactiveColor.a < 0.5f)
                particles.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        
        portalUnactive.SetActive(false);

        cinematicCamera.GetComponent<FinishCutscene>().StopCutscene();
    }
}
