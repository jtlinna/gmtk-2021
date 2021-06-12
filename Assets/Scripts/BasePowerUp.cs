using System.Collections.Generic;
using UnityEngine;

public class BasePowerUp : MonoBehaviour
{
    [SerializeField]
    private PowerUpIdentifier _id;
    public PowerUpIdentifier Id => _id;

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
        Character character = collider.GetComponentInParent<Character>();
        if(collider == null)
        {
            return;
        }

        int prevCount = _affectedCharacters.Count;
        _affectedCharacters.Add(character);
        if(prevCount == 0)
        {
            GameManager.Instance.RegisterPowerUp(this);
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        Character character = collider.GetComponentInParent<Character>();
        if(collider == null)
        {
            return;
        }

        _affectedCharacters.Remove(character);
        if(_affectedCharacters.Count == 0)
        {
            GameManager.Instance.UnregisterPowerUp(this);
        }
    }
}
