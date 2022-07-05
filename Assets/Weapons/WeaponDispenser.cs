using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponDispenser : MonoBehaviour, IPunObservable
{
    public GameObject particles;

    public GameObject[] weapons;

    private bool ready;

    public float rechargeTime;

    public int activeWeapon;

    private void OnTriggerStay(Collider other)
    {
        WeaponPickup weaponPickup = other.GetComponent<WeaponPickup>();

        if (other.gameObject.CompareTag("Player") && ready && weaponPickup.gunManager.activeGun)
        {
            if (!(weaponPickup.weapons[activeWeapon].activeSelf &&
                (weaponPickup.gunManager.currentAmmo == weaponPickup.gunManager.activeGun.maxAmmo ||
                weaponPickup.gunManager.reloading)))
            {
                weaponPickup.ChooseWeapon(activeWeapon);
                weapons[activeWeapon].SetActive(false);

                activeWeapon = weapons.Length;

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
        ChooseRandomWeapon();
        ready = true;
        particles.SetActive(true);
    }

    private void ChooseRandomWeapon()
    {
        activeWeapon = Random.Range(-weapons.Length * weapons.Length, weapons.Length * weapons.Length);

        if (activeWeapon == 0)
        {
            activeWeapon = weapons.Length - 1;
        }

        for (int i = weapons.Length; i >= 1; i--)
        {
            if (i * i <= Mathf.Abs(activeWeapon))
            {
                activeWeapon = weapons.Length - 1 - i;
                weapons[activeWeapon].SetActive(true);
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

            for (int i = 0; i < weapons.Length; i++)
            {
                stream.SendNext(active[i]);
            }
        }
        else
        {
            ready = (bool)stream.ReceiveNext();

            for (int i = 0; i < weapons.Length; i++)
            {
                active[i] = (bool)stream.ReceiveNext();
                weapons[i].SetActive(active[i]);
            }
        }
    }
}