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

    public void Load()
    {
        Load(sceneName);
    }

    public static void Load(string name)
    {
        LoadScene.name = name;
        SceneManager.LoadScene("LoadingScreen");
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(name);
        sceneLoad.allowSceneActivation = false;

        while (sceneLoad.progress < 0.8)
        {
            Debug.Log(sceneLoad.progress);
            yield return null;
        }

        sceneLoad.allowSceneActivation = true;
    }
}
