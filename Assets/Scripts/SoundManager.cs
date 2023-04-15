using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    private bool isStopped = false;

    public bool MusicMuted => musicSource.mute;
    public bool SFXMuted => effectsSource.mute;

    private void Start()
    {
        Instance = this;
        UiManager.onGameStop += Stopped;
        UiManager.onResume += Resumed;
    }

    private void Update()
    {
        if (effectsSource.isPlaying && isStopped)
        {
            effectsSource.Pause();
        }
        if(!effectsSource.isPlaying && effectsSource.time != 0 && !isStopped) 
        { 
            effectsSource.UnPause();
        }
    }

    private void Stopped()
    {
        if (!isStopped)
        {
            isStopped = true;
        }
    }

    private void Resumed()
    {
        if (isStopped)
        {
            isStopped = false;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.clip = (clip);
        effectsSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        PlayerPrefs.SetInt("musicmute", MusicMuted ? 1 : 0);
    }

    public void ToggleSFX()
    {
        effectsSource.mute = !effectsSource.mute;
        PlayerPrefs.SetInt("sfxmute", SFXMuted ? 1 : 0);
    }

    public void SetVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("volume", newVolume);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("volume"))
            AudioListener.volume = PlayerPrefs.GetFloat("volume");

        if (PlayerPrefs.HasKey("musicmute"))
            musicSource.mute = PlayerPrefs.GetInt("musicmute") == 1;

        if (PlayerPrefs.HasKey("sfxmute"))
            effectsSource.mute = PlayerPrefs.GetInt("sfxmute") == 1;
    }
}
