
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource sfxAudio, musicAudio;
    public AudioClip initialMusic;
    [SerializeField] AudioMixer master;
    public bool isMuted = false;
    public string musicSavedValue = "musicValue";
    public string sfxSavedValue = "sfxValue";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        sfxAudio = transform.GetChild(0).GetComponent<AudioSource>();
        musicAudio = transform.GetChild(1).GetComponent<AudioSource>();
        InitialPlayMusic(initialMusic);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        sfxAudio.Stop();
    }
    public void PlayMusic(AudioClip clip)
    {
        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }
    void InitialPlayMusic(AudioClip clip)
    {
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }

    public void VolumeMusic(float VolumeMusic)
    {
        master.SetFloat("Music", VolumeMusic);
    }
    public void SFXVolume(float VolumeSFX)
    {
        master.SetFloat("SFX", VolumeSFX);
    }

    public void MuteAll()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            master.SetFloat("Master", -80f);
        }
        else
        {
            master.SetFloat("Master", 0f);
        }
    }

    public void SaveSoundPreferences(float levelMusic, float levelSFX)
    {
        PlayerPrefs.SetFloat(musicSavedValue, levelMusic);
        PlayerPrefs.SetFloat(sfxSavedValue, levelSFX);
    }

    public void LoadSoundPreferences()
    {
        if (PlayerPrefs.HasKey(musicSavedValue))
        {
            VolumeMusic(PlayerPrefs.GetFloat(musicSavedValue));
            SFXVolume(PlayerPrefs.GetFloat(sfxSavedValue));
        }
    }


}
