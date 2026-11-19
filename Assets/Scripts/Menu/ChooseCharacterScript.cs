using UnityEngine;

public class ChooseCharacterScript : MonoBehaviour
{
    public GameObject[] characters;
    int characterIndex;

    public GameObject inputField;
    string characterName;
    public int playerCount = 3;
    public SceneChangerScript sceneChangerScript;
    // Text above the character that shows "current / total", e.g. "1 / 7"
    public TMPro.TMP_Text counterText;

    
    void Awake()
    {
        characterIndex = 0;
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[characterIndex].SetActive(true);
        UpdateCounter();
    }

    void UpdateCounter()
    {
        if (counterText != null)
            counterText.text = (characterIndex + 1) + " / " + characters.Length;
    }

    public void NextCharacter()
    {
        characters[characterIndex].SetActive(false);
        characterIndex++;
        if (characterIndex == characters.Length)
            characterIndex = 0;

        characters[characterIndex].SetActive(true);
        UpdateCounter();
    }

    public void PrevioustCharacter()
    {
        characters[characterIndex].SetActive(false);
        characterIndex--;
        if (characterIndex == -1)
            characterIndex = characters.Length - 1;

        characters[characterIndex].SetActive(true);
        UpdateCounter();
    }

    public void Play()
    {
        characterName = inputField.GetComponent<TMPro.TMP_InputField>().text;
        if(characterName.Length > 2)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
            PlayerPrefs.SetString("PlayerName", characterName);
            PlayerPrefs.SetInt("PlayerCount", playerCount);
            StartCoroutine(
            sceneChangerScript.Delay("play", characterIndex, characterName, playerCount));
         
        } else
            inputField.GetComponent<TMPro.TMP_InputField>().Select();
    }
}
