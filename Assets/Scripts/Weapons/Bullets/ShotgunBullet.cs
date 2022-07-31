using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ShotgunBullet : MonoBehaviourPunCallbacks
{
    public float recoilForce;
    public float shootForce;
    public float radius;
    [Space]
    public GameObject shootEffect;
    [Space]
    public LayerMask layerMask;
    private void Awake()
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
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(transform.forward * shootForce, ForceMode.VelocityChange);
            }
        }

        StartCoroutine(Destroy());
    }
    public IEnumerator Destroy()
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
