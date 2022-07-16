using Photon.Pun;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastBullet : MonoBehaviourPunCallbacks
{
    public float force;

    public LineRenderer lineRenderer;

    public GameObject startEffect;
    public GameObject contactEffect;

    private void Start()
    {
        Instantiate(startEffect, transform.position, Quaternion.identity);

        IOrderedEnumerable<RaycastHit> sorted = Physics.RaycastAll(transform.position, transform.forward, Mathf.Infinity).OrderBy(h => h.distance);

        foreach (RaycastHit hit in sorted)
        {
            if (hit.collider.gameObject.layer != 6)
            {
                if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb))
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