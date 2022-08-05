using UnityEngine;

public class DeleteEffect : MonoBehaviour
{
    private void Awake() => Destroy(gameObject, 5);
}