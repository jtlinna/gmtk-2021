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
