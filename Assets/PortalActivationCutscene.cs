using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivationCutscene : MonoBehaviour
{
    [SerializeField] GameObject portalActive;
    [SerializeField] GameObject portalUnactive;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject cinematicCamera;

    private Renderer portalUnactiveRenderer;
    private Color portalUnactiveColor;
    private GameObject camera;

    private void Start()
    {
        portalUnactiveRenderer = portalUnactive.GetComponent<Renderer>();
        portalUnactiveColor = portalUnactiveRenderer.material.color;
    }
    public void StartCutscene()
    {
        camera = Camera.main.gameObject;
        camera.SetActive(false);
        cinematicCamera.SetActive(true);

        portalActive.SetActive(true);
        StartCoroutine("FadeOut");

    }

    IEnumerator FadeOut()
    {
        while (portalUnactiveRenderer.material.color.a > 0f)
        {
            portalUnactiveRenderer.material.color = new Color(portalUnactiveColor.r, portalUnactiveColor.g, portalUnactiveColor.b, portalUnactiveColor.a - 0.01f);
            portalUnactiveColor.a -= 0.01f;
            if(portalUnactiveColor.a < 0.5f)
                particles.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        
        portalUnactive.SetActive(false);

        camera.SetActive(true);
        cinematicCamera.SetActive(false);
    }
}
