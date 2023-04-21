using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneName; //for buttons
    public static string name = null; // after loading screen

    private void Start()
    {
        if (name == null)
            return;

        StartCoroutine(LoadAsync());
    }

    public static void Load(string name)
    {
        LoadScene.name = name;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void Load()
    {
        name = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(name);
        sceneLoad.allowSceneActivation = false;

        while (sceneLoad.progress < 0.8)
        {
            yield return null;
        }

        sceneLoad.allowSceneActivation = true;
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
