using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectSelectableOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Select());
    }

    private IEnumerator Select()
    {
        yield return null;
        GetComponent<Selectable>().Select();
    }
}
