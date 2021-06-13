using UnityEngine;
using UnityEngine.Audio;

public sealed class AudioManager
{
    private const string SOUNDS_ENABLED = "SoundsEnabled";
    private const string MUSIC_ENABLED = "MusicEnabled";

    private AudioMixer _audioMixer;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new AudioManager();
            }

            return _instance;
        }
    }

    public bool IsMusicEnabled => PlayerPrefs.GetInt(MUSIC_ENABLED, 1) != 0;

    public bool IsSoundsEnabled => PlayerPrefs.GetInt(SOUNDS_ENABLED, 1) != 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        AudioManager mgr = AudioManager.Instance;
    }

    private AudioManager()
    {
        _audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        SetMusicEnabled(IsMusicEnabled);
        SetSoundsEnabled(IsSoundsEnabled);
        GameObject musicSource = Resources.Load<GameObject>("MusicSource");
        Object.DontDestroyOnLoad(musicSource);
    }

    public void SetSoundsEnabled(bool isEnabled)
    {
        PlayerPrefs.SetInt(SOUNDS_ENABLED, isEnabled ? 1 : 0);
        _audioMixer.SetFloat("SoundsVolume", isEnabled ? -80 : 0);
    }

    public void SetMusicEnabled(bool isMuted)
    {
        PlayerPrefs.SetInt(MUSIC_ENABLED, isMuted ? 1 : 0);
        _audioMixer.SetFloat("MusicVolume", isMuted ? -80 : 0);
    }
}
