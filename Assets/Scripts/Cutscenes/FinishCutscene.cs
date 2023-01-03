using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCutscene : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            StopCutscene();
    }

    public void StopCutscene() 
    {
        mainCamera.SetActive(true);
        canvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
