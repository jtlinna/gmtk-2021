using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotationSpeed;

    public void Update() => transform.Rotate(_rotationSpeed * Time.deltaTime, Space.Self);
}
