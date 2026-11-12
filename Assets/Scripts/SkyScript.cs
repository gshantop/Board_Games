using TMPro;
using UnityEngine;

public class SkyScript : MonoBehaviour
{
    public Material[] skies;
    public string[] skyNames;
    public TMP_Text skyText;  // Only the Settings scene shows the name of the sky

    int currentSky;

    void Start()
    {
        SetSky(PlayerPrefs.GetInt("Sky", 0));
    }

    public void NextSky()
    {
        SetSky(currentSky + 1);
    }

    public void PreviousSky()
    {
        SetSky(currentSky - 1);
    }

    void SetSky(int index)
    {
        currentSky = (index + skies.Length) % skies.Length;

        RenderSettings.skybox = skies[currentSky];
        PlayerPrefs.SetInt("Sky", currentSky);

        if (skyText != null)
        {
            skyText.text = skyNames[currentSky];
        }
    }
}
