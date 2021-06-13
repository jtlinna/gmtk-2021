using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public sealed class AudioManager
{
    private const string SOUNDS_ENABLED = "SoundsEnabled";
    private const string MUSIC_ENABLED = "MusicEnabled";

    private class CoroutineWrapper : MonoBehaviour { }

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

    private AudioSource _musicSource;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        AudioManager mgr = AudioManager.Instance;
    }

    private AudioManager()
    {
        _audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        Debug.Log($"Audio manager is initializing -- {nameof(IsMusicEnabled)} {IsMusicEnabled} -- {nameof(IsSoundsEnabled)} {IsSoundsEnabled}");
        _musicSource = Object.Instantiate(Resources.Load<AudioSource>("MusicSource"));
        _musicSource.gameObject.AddComponent<CoroutineWrapper>().StartCoroutine(InitJob());
        Object.DontDestroyOnLoad(_musicSource.gameObject);
    }

    public void SetSoundsEnabled(bool isEnabled)
    {
        PlayerPrefs.SetInt(SOUNDS_ENABLED, isEnabled ? 1 : 0);
        _audioMixer.SetFloat("SoundsVolume", isEnabled ? 0 : -80);
    }

    public void SetMusicEnabled(bool isEnabled)
    {
        PlayerPrefs.SetInt(MUSIC_ENABLED, isEnabled ? 1 : 0);
        _audioMixer.SetFloat("MusicVolume", isEnabled ? 0 : -80);
        if(isEnabled && !_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }

    private IEnumerator InitJob()
    {
        yield return null;
        bool isMusicEnabled = IsMusicEnabled;
        SetMusicEnabled(isMusicEnabled);
        SetSoundsEnabled(IsSoundsEnabled);

        if(isMusicEnabled)
        {
            _musicSource.Play();
        }
    }
}
