using UnityEngine;

public class HideInWebGL : MonoBehaviour
{
    private void OnEnable()
    {
#if UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }
}
