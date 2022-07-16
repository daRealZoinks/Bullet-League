using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NormalBullets : MonoBehaviourPun
{
    public float speed;

    public GameObject startEffect;
    public GameObject contactEffect;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Instantiate(startEffect, transform.position, transform.rotation);

        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(contactEffect, transform.position, Quaternion.identity);

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