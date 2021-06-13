using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public sealed class SoundsToggle : MonoBehaviour
{
    void OnEnable() => GetComponent<Toggle>().isOn = AudioManager.Instance.IsSoundsEnabled;
}
