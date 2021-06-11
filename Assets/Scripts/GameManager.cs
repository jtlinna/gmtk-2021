using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public delegate void PowerUpEventHandler(BasePowerUp powerUp);

    public event PowerUpEventHandler PowerUpRegistered;
    public event PowerUpEventHandler PowerUpUnregistered;

    [SerializeField]
    private Character[] _characters = new Character[0];
    [SerializeField]
    private Character _startingCharacter = null;

    private HashSet<BasePowerUp> _activePowerUps;
    public IReadOnlyList<BasePowerUp> ActivePowerUps => new List<BasePowerUp>(_activePowerUps);

    private int _selectedCharactedIdx;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Init()
    {
        _activePowerUps = new HashSet<BasePowerUp>();
        InitCharacter();
    }

    private void InitCharacter()
    {
        foreach(Character character in _characters)
        {
            character.SetEnabled(false);
        }

        if(_startingCharacter != null)
        {
            _selectedCharactedIdx = Array.FindIndex(_characters, c => c == _startingCharacter);
            if(_selectedCharactedIdx < 0)
            {
                Debug.LogError("Starting character not found from character array");
                return;
            }
        }
        else if(_characters.Length == 0)
        {
            Debug.LogError("No characters assigned to the array");
            return;
        }

        _characters[_selectedCharactedIdx].SetEnabled(true);
    }

    public void SelectNextCharacter()
    {
        _characters[_selectedCharactedIdx].SetEnabled(false);
        _selectedCharactedIdx = (_selectedCharactedIdx + 1) % _characters.Length;
        _characters[_selectedCharactedIdx].SetEnabled(true);
    }

    public void RegisterPowerUp(BasePowerUp powerUp)
    {
        if(_activePowerUps.Add(powerUp))
        {
            PowerUpRegistered?.Invoke(powerUp);
        }
    }

    public void UnregisterPowerUp(BasePowerUp powerUp)
    {
        if(_activePowerUps.Remove(powerUp))
        {
            PowerUpUnregistered?.Invoke(powerUp);
        }
    }

    public bool IsPowerUpActive(PowerUpIdentifier id) => _activePowerUps.Any(powerUp => powerUp.Id == id);

    public int GetActivePowerUpCount(PowerUpIdentifier id) => _activePowerUps.Count(powerUp => powerUp.Id == id);
}
