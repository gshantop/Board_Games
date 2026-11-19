using UnityEngine;
using System.IO;

public class SaveLoadScript : MonoBehaviour
{
    public string saveFileName = "playerData.json";

    // Optional: shows the loaded data on the screen (used in the Circus scene)
    public TMPro.TMP_Text infoText;


    // JsonUtility needs [System.Serializable] to save and load this class
    [System.Serializable]
    public class GameData
    {
        public int characterIndex;
        public string characterName;
        public int playerCount;
    }

    private GameData gameData = new GameData();

    public void SavePlayerData(int characterIndex, 
        string characterName, int playerCount)
    {
        gameData.characterIndex = characterIndex;
        gameData.characterName = characterName;
        gameData.playerCount = playerCount;
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, json);
        Debug.Log("Player data saved to " + 
            Application.persistentDataPath + "/" + saveFileName);
    }

    public void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/" + saveFileName;
        
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Player data loaded from " + filePath);

            if (infoText != null)
                infoText.text = "PLAYER: " + gameData.characterName +
                    "   CHARACTER: " + (gameData.characterIndex + 1) +
                    "   PLAYERS: " + gameData.playerCount;
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);

            if (infoText != null)
                infoText.text = "NO SAVED GAME";
        }
    }

    // Other scripts can read what was loaded
    public GameData GetGameData()
    {
        return gameData;
    }

}
