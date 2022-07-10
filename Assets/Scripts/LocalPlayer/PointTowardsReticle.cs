using System.Linq;
using UnityEngine;

public class PointTowardsReticle : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, Mathf.Infinity);
        IOrderedEnumerable<RaycastHit> sorted = hits.OrderBy(h => h.distance);

        foreach (RaycastHit hit in sorted)
        {
            if (hit.collider.gameObject.layer != 6)
            {
                transform.LookAt(hit.point);
                break;
            }
        }
    }
}
