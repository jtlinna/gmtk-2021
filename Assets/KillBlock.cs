using UnityEngine;

public class KillBlock : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<Character>())
        {
            GameManager.Instance.LevelFail();
        }
    }
}
