using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponDispenser : MonoBehaviour, IPunObservable
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

    private bool ready;

    public float rechargeTime;

    private int activeWeaponNumber;

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
        //ChooseRandomWeapon();
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
                break;
            case Weapon.Shotgun:
                activeWeaponNumber = 1;
                break;
            case Weapon.GrenadeLauncher:
                activeWeaponNumber = 2;
                break;
            case Weapon.Sniper:
                activeWeaponNumber = 3;
                break;
            case Weapon.RPG:
                activeWeaponNumber = 4;
                break;
        }

        weapons[activeWeaponNumber].SetActive(true);
    }

    private void ChooseRandomWeapon()
    {
        activeWeaponNumber = Random.Range(-weapons.Length * weapons.Length, weapons.Length * weapons.Length);

        if (activeWeaponNumber == 0)
        {
            activeWeaponNumber = weapons.Length - 1;
        }

        for (int i = weapons.Length; i >= 1; i--)
        {
            if (i * i <= Mathf.Abs(activeWeaponNumber))
            {
                activeWeaponNumber = weapons.Length - 1 - i;
                weapons[activeWeaponNumber].SetActive(true);
                return;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        bool[] active = new bool[weapons.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            active[i] = weapons[i].activeSelf;
        }

        if (stream.IsWriting)
        {
            stream.SendNext(ready);
            stream.SendNext(activeWeaponNumber);

            for (int i = 0; i < weapons.Length; i++)
            {
                stream.SendNext(active[i]);
            }
        }
        else
        {
            ready = (bool)stream.ReceiveNext();
            activeWeaponNumber = (int)stream.ReceiveNext();

            for (int i = 0; i < weapons.Length; i++)
            {
                active[i] = (bool)stream.ReceiveNext();
                weapons[i].SetActive(active[i]);
            }
        }
    }
}