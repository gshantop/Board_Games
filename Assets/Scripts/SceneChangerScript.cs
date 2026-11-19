using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneChangerScript : MonoBehaviour
{
    // Added from Bugsters104/Board_games (used by ChooseCharacterScript).
    // Both are optional: if they are not assigned, the fade / save step is skipped.
    public SaveLoadScript saveLoadScript;
    public FadeScript fadeScript;

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

    // ---- Added from Bugsters104/Board_games ----

    public void CLoseGame()
    {
        StartCoroutine(Delay("quit", -1, "", -1));
    }

    public void ToMainMenu()
    {
        StartCoroutine(Delay("menu", -1, "", -1));
    }

    public IEnumerator Delay(string action, int charIndex, string playerName, int playerCount)
    {
        if (string.Equals(action, "quit", StringComparison.OrdinalIgnoreCase))
        {
            yield return new WaitForSeconds(0.5f);
            PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.isPlaying = false;
#else
            if (fadeScript != null)
                yield return fadeScript.FadeOut(0.1f);
            Application.Quit();
#endif
        }
        else if (string.Equals(action, "play", StringComparison.OrdinalIgnoreCase))
        {
            if (fadeScript != null)
                yield return fadeScript.FadeOut(0.1f);
            if (saveLoadScript != null)
                saveLoadScript.SavePlayerData(charIndex, playerName, playerCount);
            // Loaded by name, because in this project build index 1 is the Settings scene
            SceneManager.LoadScene("Circus", LoadSceneMode.Single);
        }
        else if (string.Equals(action, "menu", StringComparison.OrdinalIgnoreCase))
        {
            if (fadeScript != null)
                yield return fadeScript.FadeIn(0.1f);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
