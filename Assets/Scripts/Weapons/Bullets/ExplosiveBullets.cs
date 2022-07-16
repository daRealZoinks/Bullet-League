using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(contactEffect, transform.position, Quaternion.identity);

        foreach (Collider collider in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            if (collider.TryGetComponent(out Rigidbody rb))
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
