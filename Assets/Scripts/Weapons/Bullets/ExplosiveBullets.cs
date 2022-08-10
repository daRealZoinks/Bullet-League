using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveBullets : Bullet
{
    public Rigidbody rb;

    [Space]

    public float speed;

    [Space]

    public float explosionForce;
    public float explosionRadius;
    public float upwardsModifier = 2.0f;

    [Space]

    public GameObject explosionEffect;
    public SoundCue explosionSoundCue;

    public override void Awake()
    {
        base.Awake();

        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionSoundCue.PlayRandomSoundAtPosition(transform.position);

        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        foreach (var physicsObject in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            if (physicsObject.TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier,
                    ForceMode.VelocityChange);
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