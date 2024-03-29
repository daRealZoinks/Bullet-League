using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviourPun
{
    [HideInInspector] public bool reloading = false;
    [HideInInspector] public bool shooting = false;
    public GameObject[] weapons;
    public int currentAmmo;
    public Gun activeGun;
    public WeaponPickup weaponPickup;

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !reloading && !shooting && currentAmmo > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        PlayAnimation(activeGun.shootAnimation);

        if (PhotonNetwork.OfflineMode)
        {
            Instantiate(activeGun.bullet, activeGun.shootPoint.position, activeGun.shootPoint.rotation);
        }

        if (photonView.IsMine)
        {
            PhotonNetwork.Instantiate(activeGun.bullet.name, activeGun.shootPoint.position, activeGun.shootPoint.rotation);
        }

        currentAmmo--;

        yield return new WaitForSeconds(activeGun.shootAnimation.length);

        if (currentAmmo == 0 && !reloading)
        {
            weaponPickup.ChooseWeapon(5);
        }

        shooting = false;
    }

    public IEnumerator Reload()
    {
        shooting = false;
        reloading = true;

        currentAmmo = 0;

        PlayAnimation(activeGun.loadAnimation);
        yield return new WaitForSeconds(activeGun.loadAnimation.length);

        currentAmmo = activeGun.maxAmmo;

        reloading = false;
    }

    private void PlayAnimation(AnimationClip animationClip)
    {
        activeGun.animationController.clip = animationClip;
        activeGun.animationController.Play();
    }
}