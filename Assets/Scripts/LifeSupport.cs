using UnityEngine;

public sealed class LifeSupport : BasePowerUp
{
    public void OnTriggerStay(Collider collider)
    {
        // Override OnTriggerExit handling
        if(collider.GetComponentInParent<Character>() != null)
        {
            GameManager.Instance.RegisterPowerUp(this);
        }
    }
}
