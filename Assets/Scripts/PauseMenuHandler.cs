using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PauseMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuScreen = null;

    private bool _isPaused;

    private void Start()
    {
        InputManager.ControlScheme.Yeet.PauseGame.performed += OnPauseGame;
        _isPaused = false;
        if(_pauseMenuScreen != null)
        {
            _pauseMenuScreen.SetActive(false);
        }
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
        GameManager.Instance.SetPaused(isPaused);
        if(_pauseMenuScreen == null)
        {
            return;
        }

        _pauseMenuScreen.SetActive(_isPaused);
    }

    private void OnDestroy()
    {
        InputManager.ControlScheme.Yeet.PauseGame.performed -= OnPauseGame;
    }

    private void OnPauseGame(InputAction.CallbackContext obj) => SetPaused(!_isPaused);
}
