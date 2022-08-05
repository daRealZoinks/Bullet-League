using System;
using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponDispenser : MonoBehaviour
{
    public enum Weapon
    {
        SemiAutomatic,
        Shotgun,
        GrenadeLauncher,
        Sniper,
        Rpg
    }

    public Weapon weapon;
    public GameObject particles;
    public GameObject[] weapons;
    public float rechargeTime;
    public bool ready;
    public int activeWeaponIndex;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GrabWeapon(other.GetComponent<WeaponPickup>());
        }
    }

    private void GrabWeapon(WeaponPickup weaponPickup)
    {
        if (!ready || !weaponPickup.gunManager.activeGun) return;

        if (weaponPickup.weapons[activeWeaponIndex].activeSelf &&
            (weaponPickup.gunManager.currentAmmo == weaponPickup.gunManager.activeGun.maxAmmo ||
             weaponPickup.gunManager.reloading)) return;

        weaponPickup.ChooseWeapon(activeWeaponIndex);

        foreach (var weaponObject in weapons)
        {
            weaponObject.SetActive(false);
        }

        ready = false;
        particles.SetActive(false);

        StartCoroutine(LoadNewWeapon());
    }

    private void OnEnable()
    {
        StartCoroutine(LoadNewWeapon());
    }

    private IEnumerator LoadNewWeapon()
    {
        if (!PhotonNetwork.IsMasterClient) yield break;

        yield return new WaitForSeconds(rechargeTime);

        ChooseWeapon();
        ready = true;
        particles.SetActive(true);
    }

    private void ChooseWeapon()
    {
        switch (weapon)
        {
            case Weapon.SemiAutomatic:
                activeWeaponIndex = 0;
                rechargeTime = 10;
                break;
            case Weapon.Shotgun:
                activeWeaponIndex = 1;
                rechargeTime = 15;
                break;
            case Weapon.GrenadeLauncher:
                activeWeaponIndex = 2;
                rechargeTime = 30;
                break;
            case Weapon.Sniper:
                activeWeaponIndex = 3;
                rechargeTime = 45;
                break;
            case Weapon.Rpg:
                activeWeaponIndex = 4;
                rechargeTime = 60;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        weapons[activeWeaponIndex].SetActive(true);
    }
}