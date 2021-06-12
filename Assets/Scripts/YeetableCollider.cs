using UnityEngine;

public class YeetableCollider : MonoBehaviour
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
