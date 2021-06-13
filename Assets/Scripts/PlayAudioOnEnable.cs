using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class PlayAudioOnEnable : MonoBehaviour
{
    [SerializeField]
    private bool _ignoreFirst = true;

    private bool _hasBeenEnabledPreviously;

    private void OnEnable()
    {
        if(!_ignoreFirst || _hasBeenEnabledPreviously)
        {
            AudioUtils.Play(GetComponent<AudioSource>());
        }

        _hasBeenEnabledPreviously = true;
    }
}
