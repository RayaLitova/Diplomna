using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneName;


    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void Load(string name)
    { 
        SceneManager.LoadScene(name);
    }
}
