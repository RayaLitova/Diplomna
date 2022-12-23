using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCutscene : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            StopCutscene();
    }

    public void StopCutscene() 
    {
        mainCamera.SetActive(true);
        gameObject.SetActive(false);
    }
}
