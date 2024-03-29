using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private enum GameState { InProgress, Failed, Accomplished, Paused }
    public static GameManager Instance { get; private set; }

    public delegate void PowerUpEventHandler(BasePowerUp powerUp);

    public event PowerUpEventHandler PowerUpRegistered;
    public event PowerUpEventHandler PowerUpUnregistered;

    [SerializeField]
    private Character[] _characters = new Character[0];
    [SerializeField]
    private Character _startingCharacter = null;
    [SerializeField]
    private float _maxTimeWithoutLifeSupport = 10f;
    [SerializeField]
    private int _requiredCharactersAtEndForSuccess = 1;
    [SerializeField]
    private GameObject _levelCompleteScreen;
    [SerializeField]
    private GameObject _levelFailedScreen;
    [SerializeField]
    private List<GameObject> _extraCompletionObjects = new List<GameObject>();

    private HashSet<BasePowerUp> _activePowerUps;
    public IReadOnlyList<BasePowerUp> ActivePowerUps => new List<BasePowerUp>(_activePowerUps);

    private HashSet<Character> _charactersAtEnd;

    private int _selectedCharactedIdx;
    private float _lifeSupportTimer;
    private GameState _gameState;

    private bool _zoomedOut = false;

    public float LifeSupportPercentage => Mathf.Clamp01(_lifeSupportTimer / _maxTimeWithoutLifeSupport);

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Update()
    {
        if(_gameState != GameState.InProgress)
        {
            return;
        }

        if(!IsPowerUpActive(PowerUpIdentifier.LifeSupport))
        {
            if(!_zoomedOut)
            {
                _lifeSupportTimer -= Time.deltaTime;
            }

            EvaluateFailureState();
        }
        else
        {
            _lifeSupportTimer = _maxTimeWithoutLifeSupport;
            EvaluateSuccessState();
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }

        CleanUp();
    }

    private void Init()
    {
        _activePowerUps = new HashSet<BasePowerUp>();
        _charactersAtEnd = new HashSet<Character>();
        _lifeSupportTimer = _maxTimeWithoutLifeSupport;
        _gameState = GameState.InProgress;
        LevelEnd.LevelEndEnter += OnLevelEndEnter;
        LevelEnd.LevelEndExit += OnLevelEndExit;

        InputManager.ControlScheme.Yeet.ZoomOut.performed += OnZoomOut;
        InputManager.ControlScheme.Yeet.SelectNextCharacter.performed += OnSelectNextCharacter;
        InputManager.ControlScheme.Enable();

        SetScreenActive(_levelCompleteScreen, false);
        SetScreenActive(_levelFailedScreen, false);

        if(_extraCompletionObjects.Count > 0)
        {
            foreach(GameObject item in _extraCompletionObjects)
            {
                item.SetActive(false);
            }
        }

        InitCharacter();
    }

    private void CleanUp()
    {
        LevelEnd.LevelEndEnter -= OnLevelEndEnter;
        LevelEnd.LevelEndExit -= OnLevelEndExit;

        InputManager.ControlScheme.Yeet.ZoomOut.performed -= OnZoomOut;
        InputManager.ControlScheme.Yeet.SelectNextCharacter.performed -= OnSelectNextCharacter;
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

    private void EvaluateSuccessState()
    {
        int characterCountAtEnd = _charactersAtEnd.Count;
        if(_requiredCharactersAtEndForSuccess <= 0 && characterCountAtEnd < _characters.Length)
        {
            return;
        }

        if(characterCountAtEnd < _requiredCharactersAtEndForSuccess)
        {
            return;
        }

        _gameState = GameState.Accomplished;
        Debug.Log("You won!");
        foreach(Character character in _characters)
        {
            character.LevelCompleted();
        }

        SetScreenActive(_levelCompleteScreen, true);
        if(_extraCompletionObjects.Count > 0)
        {
            foreach(GameObject item in _extraCompletionObjects)
            {
                item.SetActive(true);
            }
        }

        InputManager.ControlScheme.Disable();
    }

    private void EvaluateFailureState()
    {
        if(_lifeSupportTimer > 0)
        {
            return;
        }

        LevelFail();
    }

    public void LevelFail()
    {
        _gameState = GameState.Failed;
        Debug.Log("Game over man, game over!");
        foreach(Character character in _characters)
        {
            character.Kill();
        }

        SetScreenActive(_levelFailedScreen, true);
        InputManager.ControlScheme.Disable();
    }

    public void SelectNextCharacter()
    {
        _characters[_selectedCharactedIdx].SetEnabled(false);
        _selectedCharactedIdx = (_selectedCharactedIdx + 1) % _characters.Length;
        _characters[_selectedCharactedIdx].SetEnabled(true);
        _zoomedOut = false;
    }

    public void ToggleZoomOut()
    {
        _zoomedOut = !_zoomedOut;
        _characters[_selectedCharactedIdx].SetEnabled(!_zoomedOut);
    }

    public void SetPaused(bool isPaused)
    {
        _gameState = isPaused ? GameState.Paused : GameState.InProgress;
        _characters[_selectedCharactedIdx].SetEnabled(!isPaused);
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
    public int GetUniquePowerUpCount()
    {
        HashSet<PowerUpIdentifier> ids = new HashSet<PowerUpIdentifier>();
        int count = 0;
        foreach(BasePowerUp powerUp in _activePowerUps)
        {
            if(ids.Add(powerUp.Id))
            {
                count++;
            }
        }

        return count;
    }

    private void SetScreenActive(GameObject screen, bool isActive)
    {
        if(screen != null)
        {
            screen.SetActive(isActive);
        }
    }

    private void OnLevelEndEnter(Character character) => _charactersAtEnd.Add(character);

    private void OnLevelEndExit(Character character) => _charactersAtEnd.Remove(character);

    private void OnZoomOut(InputAction.CallbackContext obj) => ToggleZoomOut();
    private void OnSelectNextCharacter(InputAction.CallbackContext obj) => SelectNextCharacter();
}