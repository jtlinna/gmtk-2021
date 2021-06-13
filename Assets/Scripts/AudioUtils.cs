using UnityEngine;

public static class AudioUtils
{
    public static void PlayOneShot(AudioSource source, AudioClip clip)
    {
        if(source != null && clip != null)
        {
            source.PlayOneShot(clip);
        }
    }

    public static void Play(AudioSource source)
    {
        if(source != null && !source.isPlaying)
        {
            source.Play();
        }
    }

    public static void Stop(AudioSource source)
    {
        if(source != null && source.isPlaying)
        {
            source.Stop();
        }
    }
}
