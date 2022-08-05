using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ShotgunBullet : MonoBehaviourPunCallbacks
{
    public float recoilForce = 10.0f;
    public float shootForce = 40.0f;
    public float radius = 3.0f;

    [Space] public GameObject shootEffect;

    [Space] public LayerMask layerMask;

    private void Awake()
    {
        var muzzleTransform = transform;
        Instantiate(shootEffect, muzzleTransform.position, muzzleTransform.rotation);

        var localPlayer = GameObject.FindGameObjectWithTag("Player");

        Collider[] physicsObjects;

        if (photonView.IsMine || PhotonNetwork.OfflineMode)
        {
            Rigidbody playerRb = localPlayer.GetComponent<Rigidbody>();
            playerRb.AddForce(-transform.forward * recoilForce, ForceMode.VelocityChange);

            physicsObjects = Physics.OverlapSphere(transform.position, radius, layerMask);
        }
        else
        {
            physicsObjects = Physics.OverlapSphere(transform.position, radius);
        }

        foreach (var physicsObject in physicsObjects)
        {
            if (physicsObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(transform.forward * shootForce, ForceMode.VelocityChange);
            }
        }

        StartCoroutine(Destroy());
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