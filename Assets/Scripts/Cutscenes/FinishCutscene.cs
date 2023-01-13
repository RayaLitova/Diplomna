using UnityEngine;

public class FinishCutscene : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //skip cutscene on Escape pressed
            StopCutscene();
    }

    public void StopCutscene() 
    {
        mainCamera.SetActive(true);
        canvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
