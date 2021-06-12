using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneController : MonoBehaviour
{
    [SerializeField]
    private int _nextSceneIndex = 0;
    [SerializeField]
    private TransitionHandler _transitionHandler;
    private void Awake()
    {
        if(_transitionHandler != null)
        {
            _transitionHandler.SetActive(false);
        }
    }

    public void ReloadScene() => HandleLoadScene(SceneManager.GetActiveScene().buildIndex);

    public void LoadNextScene() => HandleLoadScene(_nextSceneIndex);

    public void LoadMainMenu() => HandleLoadScene(0);

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#else
        Application.Quit();
#endif
    }

    private void HandleLoadScene(int index)
    {
        if(_transitionHandler == null)
        {
            SceneManager.LoadScene(index);
            return;
        }

        _transitionHandler.MidPointReached += OnMidPointReached;
        if(_transitionHandler.StartTransition())
        {
            return;
        }

        _transitionHandler.MidPointReached -= OnMidPointReached;
        SceneManager.LoadScene(index);
    }

    private void OnMidPointReached()
    {
        _transitionHandler.MidPointReached -= OnMidPointReached;
        SceneManager.LoadScene(_nextSceneIndex);
    }
}
