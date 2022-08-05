using System.Linq;
using UnityEngine;

public class PointTowardsReticle : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        var camTransform = cam.transform;
        var hits = Physics.RaycastAll(camTransform.position, camTransform.forward, Mathf.Infinity);
        var sorted = hits.OrderBy(h => h.distance);

        foreach (var hit in sorted)
        {
            if (hit.collider.gameObject.layer == 6) continue;
            transform.LookAt(hit.point);
            break;
        }
    }
}