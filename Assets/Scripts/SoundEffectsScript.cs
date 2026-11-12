using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource effectsSource;

    void Awake()
    {
        // The volume that was chosen in the Settings scene
        effectsSource.volume = PlayerPrefs.GetFloat("EffectsVolume", effectsSource.volume);
    }

    public void Hover()
    {
        if (audioClips.Length > 0)
        {
            effectsSource.PlayOneShot(audioClips[0]);
        }
    }

    public void Click()
    {
        if (audioClips.Length > 0)
        {
            effectsSource.PlayOneShot(audioClips[1]);
        }
    } 
    
    public void OnDice()
    {
        if (audioClips.Length > 0)
        {
            effectsSource.PlayOneShot(audioClips[2]);
        }
    }
}
