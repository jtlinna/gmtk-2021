using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class CharacterEffectHandler : MonoBehaviour
{
    [Serializable]
    private class EffectTuple
    {
        [SerializeField]
        private PowerUpIdentifier _id;
        public PowerUpIdentifier Id => _id;

        [SerializeField]
        private GameObject _effectObject;
        public GameObject EffectObject => _effectObject;
    }

    [SerializeField]
    private List<EffectTuple> _effects = new List<EffectTuple>();

    private void Start()
    {
        GameManager.Instance.PowerUpRegistered += OnPowerUpRegistered;
        GameManager.Instance.PowerUpUnregistered += OnPowerUpUnregistered;

        _effects.ForEach(DisableEffect);
    }

    private void OnDestroy()
    {
        if(GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.PowerUpRegistered -= OnPowerUpRegistered;
        GameManager.Instance.PowerUpUnregistered -= OnPowerUpUnregistered;
    }

    private void EnableEffect(EffectTuple effect)
    {
        if(effect?.EffectObject != null)
        {
            effect.EffectObject.SetActive(true);
        }
    }

    private void DisableEffect(EffectTuple effect)
    {
        if(effect?.EffectObject != null)
        {
            effect.EffectObject.SetActive(false);
        }
    }

    private void OnPowerUpRegistered(BasePowerUp powerUp) => EnableEffect(_effects.FirstOrDefault(e => e.Id == powerUp.Id));
    private void OnPowerUpUnregistered(BasePowerUp powerUp)
    {
        if(GameManager.Instance.GetActivePowerUpCount(powerUp.Id) == 0)
        {
            DisableEffect(_effects.FirstOrDefault(e => e.Id == powerUp.Id));
        }
    }
}
