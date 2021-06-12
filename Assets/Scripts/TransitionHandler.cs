using Coffee.UIEffects;
using System;
using System.Collections;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    public event Action MidPointReached;

    [SerializeField]
    private float _transitionTotalDuration = 2f;
    [SerializeField]
    private float _delayAtMidPoint = 0.5f;
    [SerializeField]
    private Canvas _transitionCanvas = null;
    [SerializeField]
    private UITransitionEffect _transitionEffect = null;

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    public bool StartTransition()
    {
        if(_transitionCanvas == null || _transitionEffect == null)
        {
            return false;
        }

        DontDestroyOnLoad(gameObject);
        SetActive(true);
        _transitionCanvas.overrideSorting = true;
        _transitionCanvas.sortingOrder = 99999;
        StartCoroutine(Transition());
        return true;
    }

    private IEnumerator Transition()
    {
        float timer = 0f;
        float transitionHalfDuration = _transitionTotalDuration * 0.5f;
        _transitionEffect.effectFactor = 0f;

        do
        {
            yield return null;
            timer += Time.deltaTime;
            _transitionEffect.effectFactor = Mathf.Clamp01(timer / transitionHalfDuration);
        }
        while(timer < transitionHalfDuration);

        MidPointReached?.Invoke();

        yield return new WaitForSeconds(_delayAtMidPoint);
        timer = 0f;

        do
        {
            yield return null;
            timer += Time.deltaTime;
            _transitionEffect.effectFactor = 1f - Mathf.Clamp01(timer / transitionHalfDuration);
        }
        while(timer < transitionHalfDuration);

        SetActive(false);
        Destroy(gameObject);
    }
}
