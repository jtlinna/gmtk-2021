using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour
{
    [SerializeField]
    private PowerUpIdentifier _id;
    public PowerUpIdentifier Id => _id;

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponentInParent<Character>() != null)
        {
            GameManager.Instance.RegisterPowerUp(this);
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if(collider.GetComponentInParent<Character>() != null)
        {
            GameManager.Instance.UnregisterPowerUp(this);
        }
    }
}
