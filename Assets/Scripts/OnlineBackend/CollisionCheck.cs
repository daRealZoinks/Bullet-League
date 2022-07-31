using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool colliding;
    [Space]
    public string tagToCollideAgainst;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCollideAgainst))
        {
            colliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCollideAgainst))
        {
            colliding = false;
        }
    }
}