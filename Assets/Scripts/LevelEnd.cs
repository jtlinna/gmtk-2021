using UnityEngine;

public sealed class LevelEnd : MonoBehaviour
{
    public delegate void EndReachedHandler(Character character);
    public static event EndReachedHandler LevelEndEnter;
    public static event EndReachedHandler LevelEndExit;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponentInParent<Character>() is Character character)
        {
            LevelEndEnter?.Invoke(character);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.GetComponentInParent<Character>() is Character character)
        {
            LevelEndExit?.Invoke(character);
        }
    }
}
