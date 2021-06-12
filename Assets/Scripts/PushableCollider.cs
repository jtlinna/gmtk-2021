using UnityEngine;

public class PushableCollider : MonoBehaviour
{
    public Rigidbody RigidbodyProxy;

    void OnValidate()
    {
        if(RigidbodyProxy == null)
        {
            RigidbodyProxy = GetComponentInParent<Rigidbody>();
        }
    }
}
