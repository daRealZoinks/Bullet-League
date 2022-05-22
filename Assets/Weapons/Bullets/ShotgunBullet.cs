using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ShotgunBullet : MonoBehaviourPunCallbacks
{
    public float recoilForce;
    public float shootForce;
    public float radius;

    public GameObject shootEffect;

    public LayerMask layerMask;

    private void Start()
    {
        Instantiate(shootEffect, transform.position, transform.rotation);

        GameObject localPlayer = GameObject.FindGameObjectWithTag("Player");

        Collider[] colliders;

        if (photonView.IsMine || PhotonNetwork.OfflineMode)
        {
            Rigidbody playerRb = localPlayer.GetComponent<Rigidbody>();
            playerRb.AddForce(-transform.forward * recoilForce, ForceMode.VelocityChange);

            colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        }
        else
        {
            colliders = Physics.OverlapSphere(transform.position, radius);
        }

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(transform.forward * shootForce, ForceMode.VelocityChange);
            }
        }

        StartCoroutine(Destroy());
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
