using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void Awake()
    {
        if (Camera.main != null)
        {
            target = Camera.main.transform;
        }

        if (target == null)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        var rotation = target.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}