using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public class SceneLoaderMB : MonoBehaviour { }
    private static SceneLoaderMB sceneLoaderMB;

    private static void Init()
    {
        if (sceneLoaderMB == null)
        {
            GameObject gameObject = new GameObject("SceneLoader");
            sceneLoaderMB = gameObject.AddComponent<SceneLoaderMB>();
        }
    }
    public static void LoadScene(int scenenumber, int delaysecond)
    {
        Init();
        sceneLoaderMB.StartCoroutine(LoadYourAsyncScene(scenenumber, delaysecond));
    }

    static IEnumerator LoadYourAsyncScene(int scenenumber, int delayseconds)
    {
        yield return new WaitForSeconds(delayseconds);
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. 

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenenumber);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}