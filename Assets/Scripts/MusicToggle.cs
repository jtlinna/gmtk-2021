using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public sealed class MusicToggle : MonoBehaviour
{
    void OnEnable() => GetComponent<Toggle>().SetIsOnWithoutNotify(AudioManager.Instance.IsMusicEnabled);
}
