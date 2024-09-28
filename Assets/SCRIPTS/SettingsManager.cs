using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Tooltip("Slider for controlling the master volume.")] 
    [SerializeField] private Slider masterVol;

    [Tooltip("Slider for controlling the music volume.")]
    [SerializeField] private Slider musicVol;

    [Tooltip("Slider for controlling the sound effects volume.")]
    [SerializeField] private Slider sfxVol;

    [Tooltip("Reference to the audio mixer to adjust audio settings.")] 
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        // Retrieve and set the initial master volume value from the audio mixer.
        audioMixer.GetFloat("MasterVol", out var masterVolValue);
        masterVol.value = masterVolValue;

        // Retrieve and set the initial music volume value from the audio mixer.
        audioMixer.GetFloat("MusicVol", out var musicVolValue);
        musicVol.value = musicVolValue;

        // Retrieve and set the initial sound effects volume value from the audio mixer.
        audioMixer.GetFloat("SfxVol", out var sfxVolValue);
        sfxVol.value = sfxVolValue;
    }

    /// <summary>
    /// Changes the master volume based on the slider value.
    /// </summary>
    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVol", masterVol.value);
    }

    /// <summary>
    /// Changes the music volume based on the slider value.
    /// </summary>
    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVol", musicVol.value);
    }

    /// <summary>
    /// Changes the sound effects volume based on the slider value.
    /// </summary>
    public void ChangeSfxVolume()
    {
        audioMixer.SetFloat("SfxVol", sfxVol.value);
    }
}