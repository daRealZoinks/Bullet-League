using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveBullets : MonoBehaviourPun
{
    public Rigidbody rb;
    [Space]
    public float speed;
    [Space]
    public float explosionForce;
    public float explosionRadius;
    public float upwardsModifier;
    [Space]
    public GameObject contactEffect;
    private void Awake()
    {
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }
    private void Update()
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
