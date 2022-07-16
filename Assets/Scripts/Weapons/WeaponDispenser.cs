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
        RPG
    }

    public Weapon weapon;
    public GameObject particles;
    public GameObject[] weapons;

    [HideInInspector]
    public float rechargeTime;
    [HideInInspector]
    public bool ready;
    [HideInInspector]
    public int activeWeaponNumber;

    private void OnTriggerStay(Collider other)
    {
        WeaponPickup weaponPickup = other.GetComponent<WeaponPickup>();

        if (other.gameObject.CompareTag("Player") && ready && weaponPickup.gunManager.activeGun)
        {
            if (!(weaponPickup.weapons[activeWeaponNumber].activeSelf &&
                (weaponPickup.gunManager.currentAmmo == weaponPickup.gunManager.activeGun.maxAmmo ||
                weaponPickup.gunManager.reloading)))
            {
                weaponPickup.ChooseWeapon(activeWeaponNumber);
                weapons[activeWeaponNumber].SetActive(false);

                activeWeaponNumber = weapons.Length;

                StartCoroutine(LoadNewWeapon());
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(LoadNewWeapon());
    }

    private IEnumerator LoadNewWeapon()
    {
        ready = false;
        particles.SetActive(false);
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
                activeWeaponNumber = 0;
                rechargeTime = 10;
                break;
            case Weapon.Shotgun:
                activeWeaponNumber = 1;
                rechargeTime = 15;
                break;
            case Weapon.GrenadeLauncher:
                activeWeaponNumber = 2;
                rechargeTime = 30;
                break;
            case Weapon.Sniper:
                activeWeaponNumber = 3;
                rechargeTime = 45;
                break;
            case Weapon.RPG:
                activeWeaponNumber = 4;
                rechargeTime = 60;
                break;
        }

        weapons[activeWeaponNumber].SetActive(true);
    }
}