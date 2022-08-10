using Photon.Pun;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastBullet : Bullet
{
    public float force = 50.0f;

    [Space]

    public LineRenderer lineRenderer;

    [Space]

    public GameObject startEffect;
    public GameObject contactEffect;
    public SoundCue contactSoundCue;


    public override void Awake()
    {
        base.Awake();

        Instantiate(startEffect, transform.position, Quaternion.identity);

        var muzzle = transform;
        var sorted = Physics.RaycastAll(muzzle.position, muzzle.forward, Mathf.Infinity).OrderBy(h => h.distance);

        foreach (var hit in sorted)
        {
            if (hit.collider.gameObject.layer == 6) continue;

            if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForceAtPosition(transform.forward * force, hit.point, ForceMode.VelocityChange);
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            StartCoroutine(Destroy());

            Instantiate(contactEffect, hit.point, Quaternion.identity);


            contactSoundCue.PlayRandomSoundAtPosition(hit.point);

            break;
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);

        if (PhotonNetwork.OfflineMode)
        {
            Destroy(gameObject);
        }
        else
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}