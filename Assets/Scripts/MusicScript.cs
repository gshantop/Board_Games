using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;

    public AudioSource musicSource;
    public AudioClip[] songs;

    int currentSong;

    void Awake()
    {
        // Every scene has its own BackgroundMusic object, but only the first one stays,
        // so the music does not start over when the scene changes
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", musicSource.volume);
        PlaySong(PlayerPrefs.GetInt("Song", 0));
    }

    void Update()
    {
        // When the song has ended, the next one starts by itself
        if (instance == this && !musicSource.isPlaying)
        {
            NextSong();
        }
    }

    public void NextSong()
    {
        PlaySong(currentSong + 1);
    }

    public void PreviousSong()
    {
        PlaySong(currentSong - 1);
    }

    public string GetSongName()
    {
        return songs[currentSong].name;
    }

    void PlaySong(int index)
    {
        // After the last song comes the first one, and before the first one - the last one
        currentSong = (index + songs.Length) % songs.Length;

        musicSource.clip = songs[currentSong];
        musicSource.Play();

        PlayerPrefs.SetInt("Song", currentSong);
    }
}
