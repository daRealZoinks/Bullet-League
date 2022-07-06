using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool colliding;

    public string tagToCollide;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCollide))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCollide))
        {
            colliding = false;
        }
    }
}
