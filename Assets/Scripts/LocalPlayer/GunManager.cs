using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviourPun
{
    [HideInInspector]
    public bool reloading = false;
    [HideInInspector]
    public bool shooting = false;
    public GameObject[] weapons;
    public int currentAmmo;
    public Gun activeGun;
    public WeaponPickup weaponPickup;

    private void Start()
    {
        StartCoroutine(Reload());
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && (!reloading && !shooting && currentAmmo > 0))
        {
            reloading = false;
            shooting = true;
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        activeGun.animationController.clip = activeGun.shootAnimation;
        activeGun.animationController.Play();

        if (PhotonNetwork.OfflineMode)
        {
            Instantiate(
                activeGun.bullet,
                activeGun.shootPoint.position,
                activeGun.shootPoint.rotation);
        }
        else
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Instantiate(
                    activeGun.bullet.name,
                    activeGun.shootPoint.position,
                    activeGun.shootPoint.rotation);
            }
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
        activeGun.animationController.clip = activeGun.loadAnimation;
        activeGun.animationController.Play();

        shooting = false;
        reloading = true;

        currentAmmo = 0;
        yield return new WaitForSeconds(activeGun.loadAnimation.length);
        currentAmmo = activeGun.maxAmmo;

        reloading = false;
    }
}