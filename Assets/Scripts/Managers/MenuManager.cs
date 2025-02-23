using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Volume Settings")]
    public Slider musicSlider, sfxSlider;
    public Toggle muteToggle;

    void Start()
    {
        LoadOptions();
    }

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeMusicVolume()
    {
        AudioManager.Instance.VolumeMusic(musicSlider.value);
    }
    public void ChangeSFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
    public void CheckMute()
    {
        AudioManager.Instance.MuteAll();
    }

    public void SaveOptions()
    {
        AudioManager.Instance.SaveSoundPreferences(musicSlider.value, sfxSlider.value);
    }

    public void LoadOptions()
    {
        AudioManager.Instance.LoadSoundPreferences();
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.Instance.musicSavedValue);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.Instance.sfxSavedValue);
        muteToggle.isOn = AudioManager.Instance.isMuted;
    }
}
