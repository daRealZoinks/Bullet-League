using Photon.Pun;
using System.Collections;
using System.Linq;
using UnityEngine;

public class RaycastBullet : MonoBehaviourPunCallbacks
{
    public float force;

    public LineRenderer lineRenderer;

    public GameObject startEffect;
    public GameObject contactEffect;

    private void Start()
    {
        Instantiate(startEffect, transform.position, Quaternion.identity);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, Mathf.Infinity);
        IOrderedEnumerable<RaycastHit> sorted = hits.OrderBy(h => h.distance);

        foreach (RaycastHit hit in sorted)
        {
            if (hit.collider.gameObject.layer != 6)
            {
                Rigidbody rb = hit.collider.attachedRigidbody;

                if (rb != null)
                {
                    rb.AddForceAtPosition(transform.forward * force, hit.point, ForceMode.VelocityChange);
                }

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);

                StartCoroutine(Destroy());

                Instantiate(contactEffect, hit.point, Quaternion.identity);

                break;
            }
        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);

        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        if (PhotonNetwork.OfflineMode)
        {
            Destroy(gameObject);
        }
    }
}