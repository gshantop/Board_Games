using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class LeaderboardScript : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public string name;
        public int score;
    }

    // JsonUtility can not save a List on its own, so the list is kept inside a class
    [System.Serializable]
    class SaveData
    {
        public List<Entry> entries = new List<Entry>();
    }

    const string SaveFileName = "leaderboard.json";
    const int MaxEntries = 10;

    // The texts of every row, from the first place down to the last one
    public TMP_Text[] nameTexts;
    public TMP_Text[] scoreTexts;

    // The name of the player who is playing right now is shown in this color
    public Color playerColor = new Color(1f, 0.85f, 0.2f, 1f);

    // Shown until the first real result is saved, so the table is not empty
    public Entry[] exampleEntries;

    void Start()
    {
        List<Entry> entries = LoadEntries();

        if (entries.Count == 0)
            entries.AddRange(exampleEntries);

        // The best score is on top
        entries.Sort((a, b) => b.score.CompareTo(a.score));

        // The name that was typed in the main menu
        string playerName = PlayerPrefs.GetString("PlayerName", "");

        for (int i = 0; i < nameTexts.Length; i++)
        {
            if (i < entries.Count)
            {
                nameTexts[i].text = entries[i].name;
                scoreTexts[i].text = entries[i].score.ToString();

                if (playerName != "" && entries[i].name == playerName)
                    nameTexts[i].color = playerColor;
            }
            else
            {
                // Nobody has taken this place yet
                nameTexts[i].text = "---";
                scoreTexts[i].text = "0";
            }
        }
    }

    // The game scene can call this when a game is over, for example:
    // LeaderboardScript.AddResult(PlayerPrefs.GetString("PlayerName"), score);
    public static void AddResult(string playerName, int score)
    {
        List<Entry> entries = LoadEntries();

        Entry entry = new Entry();
        entry.name = playerName;
        entry.score = score;
        entries.Add(entry);

        entries.Sort((a, b) => b.score.CompareTo(a.score));

        // Only the best results are kept
        if (entries.Count > MaxEntries)
            entries.RemoveRange(MaxEntries, entries.Count - MaxEntries);

        SaveData data = new SaveData();
        data.entries = entries;
        File.WriteAllText(SavePath(), JsonUtility.ToJson(data));
    }

    static List<Entry> LoadEntries()
    {
        if (!File.Exists(SavePath()))
            return new List<Entry>();

        SaveData data = null;

        try
        {
            data = JsonUtility.FromJson<SaveData>(File.ReadAllText(SavePath()));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Leaderboard file could not be read: " + e.Message);
        }

        if (data == null || data.entries == null)
            return new List<Entry>();

        return data.entries;
    }

    static string SavePath()
    {
        return Application.persistentDataPath + "/" + SaveFileName;
    }
}
