using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class SelectButtonOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Select());
    }

    private IEnumerator Select()
    {
        yield return null;
        GetComponent<Button>().Select();
    }
}
