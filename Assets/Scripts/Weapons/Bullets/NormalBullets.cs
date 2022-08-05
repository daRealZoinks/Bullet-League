using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NormalBullets : MonoBehaviourPun
{
    public float speed;

    [Space] public GameObject startEffect;
    public GameObject contactEffect;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        var bulletTransform = transform;
        Instantiate(startEffect, bulletTransform.position, bulletTransform.rotation);

        _rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(contactEffect, transform.position, Quaternion.identity);

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