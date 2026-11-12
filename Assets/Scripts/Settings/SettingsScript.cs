using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;
    public AudioSource effectsSource;
    public TMP_Text songText;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_Dropdown qualityDropdown;

    List<Resolution> resolutions = new List<Resolution>();

    // Unity calls this once when the game starts, so the saved quality is already used in the main menu
    [RuntimeInitializeOnLoadMethod]
    static void LoadSavedQuality()
    {
        if (PlayerPrefs.HasKey("Quality"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }
    }

    void Start()
    {
        // The controls show the values that are in use right now
        musicSlider.SetValueWithoutNotify(MusicScript.instance.musicSource.volume);
        effectsSlider.SetValueWithoutNotify(effectsSource.volume);
        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);

        FillResolutions();
        FillQualities();
    }

    void Update()
    {
        // The song also changes by itself when it ends, so the name is checked all the time
        songText.text = MusicScript.instance.GetSongName();
    }

    void FillResolutions()
    {
        List<string> options = new List<string>();
        int currentResolution = 0;

        foreach (Resolution resolution in Screen.resolutions)
        {
            string option = resolution.width + " x " + resolution.height;

            // The same size is listed once for every refresh rate, but one of them is enough
            if (options.Contains(option))
                continue;

            options.Add(option);
            resolutions.Add(resolution);

            if (resolution.width == Screen.width && resolution.height == Screen.height)
                currentResolution = resolutions.Count - 1;
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.SetValueWithoutNotify(currentResolution);
    }

    void FillQualities()
    {
        // The quality levels come from Edit > Project Settings > Quality
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
    }

    public void SetMusicVolume(float volume)
    {
        MusicScript.instance.musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }

    // The buttons call these two instead of MusicScript itself,
    // because the music player usually comes along from the main menu scene
    public void NextSong()
    {
        MusicScript.instance.NextSong();
    }

    public void PreviousSong()
    {
        MusicScript.instance.PreviousSong();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", index);
    }
}
