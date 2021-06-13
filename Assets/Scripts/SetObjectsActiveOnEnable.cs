using UnityEngine;

public sealed class SetObjectsActiveOnEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectsToDisable = new GameObject[0];
    [SerializeField]
    private GameObject[] _objectsToEnable = new GameObject[0];

    private void OnValidate()
    {
        for(int i = 0; i < _objectsToDisable.Length; i++)
        {
            if(_objectsToDisable[i] == gameObject)
            {
                Debug.LogError($"Object on index {i} in objects to disable is referencing component's game object, setting to null", this);
                _objectsToDisable[i] = null;
            }
        }
    }

    private void OnEnable()
    {
        foreach(GameObject go in _objectsToDisable)
        {
            if(go != null)
            {
                go.SetActive(false);
            }
        }
        foreach(GameObject go in _objectsToEnable)
        {
            if(go != null)
            {
                go.SetActive(true);
            }
        }
    }
}
