using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PowerUpIndicatorHandler : MonoBehaviour
{
    [Serializable]
    private class IndicatorTuple
    {
        [SerializeField]
        private PowerUpIdentifier _id;
        public PowerUpIdentifier Id => _id;

        [SerializeField]
        private GameObject _indicatorObject;
        public GameObject IndicatorObject => _indicatorObject;
    }

    [SerializeField]
    private List<IndicatorTuple> _indicators = new List<IndicatorTuple>();

    private IEnumerator Start()
    {
        _indicators.ForEach(DisableIndicator);
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.PowerUpRegistered += OnPowerUpRegistered;
        GameManager.Instance.PowerUpUnregistered += OnPowerUpUnregistered;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PowerUpRegistered -= OnPowerUpRegistered;
            GameManager.Instance.PowerUpUnregistered -= OnPowerUpUnregistered;
        }
    }

    private void EnableIndicator(IndicatorTuple indicator)
    {
        if(indicator?.IndicatorObject == null)
        {
            return;
        }

        indicator.IndicatorObject.SetActive(true);
    }

    private void DisableIndicator(IndicatorTuple indicator)
    {
        if(indicator?.IndicatorObject == null)
        {
            return;
        }

        indicator.IndicatorObject.SetActive(false);
    }

    private void OnPowerUpRegistered(BasePowerUp powerUp) => EnableIndicator(_indicators.FirstOrDefault(i => i.Id == powerUp.Id));

    private void OnPowerUpUnregistered(BasePowerUp powerUp) => DisableIndicator(_indicators.FirstOrDefault(i => i.Id == powerUp.Id));
}
