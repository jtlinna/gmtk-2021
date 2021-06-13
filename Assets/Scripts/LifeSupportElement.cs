using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeSupportElement : MonoBehaviour
{
    [SerializeField]
    private Slider _progressBar;
    [SerializeField]
    private float _adjustmentSpeed = 0.5f;
    [SerializeField]
    private AudioSource _alertAudioSource;
    [SerializeField]
    private float _alertSoundThreshold = 0.4f;
    [SerializeField]
    private Color _mainColor = Color.cyan;
    [SerializeField]
    private Color _lowOxygenColor = Color.red;

    [SerializeField]
    private Image _barIcon = null;

    private void OnValidate()
    {
        if(_progressBar == null)
        {
            _progressBar = GetComponentInChildren<Slider>();
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        if(_progressBar != null)
        {
            _progressBar.value = GameManager.Instance.LifeSupportPercentage;
        }
        
    }

    

    private void Update()
    {
        if(GameManager.Instance == null || _progressBar == null)
        {
            return;
        }

        float percentage = GameManager.Instance.LifeSupportPercentage;
        _progressBar.value = Mathf.MoveTowards(_progressBar.value, percentage, _adjustmentSpeed * Time.deltaTime);
        if(percentage <= _alertSoundThreshold)
        {
            AudioUtils.Play(_alertAudioSource);
        }
        else
        {
            AudioUtils.Stop(_alertAudioSource);
        }

        if (_progressBar.value > _alertSoundThreshold)
        {
            _barIcon.color = _mainColor;
        }
        else
        {
            _barIcon.color = _lowOxygenColor;
        }
    }
}
