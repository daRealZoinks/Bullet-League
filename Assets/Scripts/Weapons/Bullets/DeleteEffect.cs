using UnityEngine;

public class DeleteEffect : MonoBehaviour
{
    void Awake() => Destroy(gameObject, 5);
}