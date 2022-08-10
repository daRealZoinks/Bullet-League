using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NormalBullets : Bullet
{
    public float speed;

    [Space]

    public GameObject startEffect;
    public GameObject contactEffect;
    public SoundCue contactSoundCue;
    private Rigidbody _rigidbody;

    public override void Awake()
    {
        base.Awake();
        
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
        shootSoundCue.PlayRandomSoundAtPosition(transform.position);
        // AudioSource.PlayClipAtPoint(contactSoundCue.clips[Random.Range(0, contactSoundCue.clips.Length)], transform.position);

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