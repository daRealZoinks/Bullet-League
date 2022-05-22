using Photon.Pun;
using UnityEngine;

public class ExplosiveBullets : MonoBehaviourPun
{
    public Rigidbody rb;

    public float speed;

    public float explosionForce;
    public float explosionRadius;
    public float upwardsModifier;

    public GameObject contactEffect;

    void Start()
    {
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnTriggerStay(Collider other)
    {
        Detonate();
    }

    private void OnTriggerEnter(Collider other)
    {
        Detonate();
    }

    private void Detonate()
    {
        Instantiate(contactEffect, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.VelocityChange);
            }
        }

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
