using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasePowerUp : MonoBehaviour
{
    [System.Serializable]
    public sealed class PowerUpCharacterEvent : UnityEvent<Character> { }

    [SerializeField]
    private PowerUpIdentifier _id;
    public PowerUpIdentifier Id => _id;

    public PowerUpCharacterEvent CharacterEnter;
    public PowerUpCharacterEvent CharacterExit;

    private HashSet<Character> _affectedCharacters = new HashSet<Character>();

    private void Awake()
    {
        _affectedCharacters = new HashSet<Character>();
    }

    private void OnDestroy()
    {
        _affectedCharacters.Clear();
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if(collider == null)
        {
            return;
        }

        Character character = collider.GetComponentInParent<Character>();

        int prevCount = _affectedCharacters.Count;
        if(character == null || !_affectedCharacters.Add(character))
        {
            return;
        }

        CharacterEnter?.Invoke(character);
        if(prevCount == 0)
        {
            GameManager.Instance.RegisterPowerUp(this);
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if(collider == null)
        {
            return;
        }

        Character character = collider.GetComponentInParent<Character>();
        if(character == null || !_affectedCharacters.Remove(character))
        {
            return;
        }

        CharacterExit?.Invoke(character);
        if(_affectedCharacters.Count == 0)
        {
            GameManager.Instance.UnregisterPowerUp(this);
        }
    }
}
