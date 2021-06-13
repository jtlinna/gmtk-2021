using UnityEngine;

[CreateAssetMenu(fileName = "AudioControls", menuName = "NGL/Audio Controls")]
public sealed class AudioControls : ScriptableObject
{
    public void SetSoundsEnabled(bool isEnabled) => AudioManager.Instance.SetSoundsEnabled(isEnabled);
    public void SetMusicEnabled(bool isEnabled) => AudioManager.Instance.SetMusicEnabled(isEnabled);
}
