using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    [Space]
    public int maxAmmo;
    [Space]
    public AudioClip shootSound;
    public AnimationClip shootAnimation;
    [Space]
    public AudioClip loadSound;
    public AnimationClip loadAnimation;
    [Space]
    public Animation animationController;
    [HideInInspector] public GunManager gunManager;
    private void OnEnable()
    {
        gunManager = GetComponentInParent<GunManager>();
        gunManager.reloading = false;
        gunManager.shooting = false;
        gunManager.activeGun = this;
        gunManager.StartCoroutine(gunManager.Reload());
    }
    private void OnDisable()
    {
        animationController.Stop();
    }
}