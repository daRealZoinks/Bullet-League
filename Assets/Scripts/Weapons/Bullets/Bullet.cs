using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public SoundCue shootSoundCue;

    public virtual void Awake()
    {
        Debug.Log("bullet gets instantiated");
        shootSoundCue.PlayRandomSoundAtPosition(transform.position);
    }
}