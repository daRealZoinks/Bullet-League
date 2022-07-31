using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private void Awake()
    {
        target = Camera.main.transform;
    }
    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(transform.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);
        }
    }
}