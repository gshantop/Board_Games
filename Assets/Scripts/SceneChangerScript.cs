using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChangerScript : MonoBehaviour
{
    public void ExitGame()
    {
        StartCoroutine(Exit());
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Change(sceneName));
    }

    IEnumerator Change(string sceneName)
    {
        // A short wait, so the click sound can still be heard
        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.8f);

        // UnityEditor only exists inside the editor, without this check the game cannot be built
#if UNITY_EDITOR
        if(UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
