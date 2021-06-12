﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeSupportElement : MonoBehaviour
{
    [SerializeField]
    private Slider _progressBar;
    [SerializeField]
    private float _adjustmentSpeed = 0.5f;

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

        _progressBar.value = Mathf.MoveTowards(_progressBar.value, GameManager.Instance.LifeSupportPercentage, _adjustmentSpeed * Time.deltaTime);
    }
}