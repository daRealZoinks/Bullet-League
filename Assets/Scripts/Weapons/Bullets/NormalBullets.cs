using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NormalBullets : MonoBehaviourPun
{
    public float speed;
    [Space]
    public GameObject startEffect;
    public GameObject contactEffect;
    private Rigidbody rb;
    private void Awake()
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