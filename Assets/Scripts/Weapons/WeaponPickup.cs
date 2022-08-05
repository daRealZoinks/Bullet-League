using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject[] weapons;

    public GunManager gunManager;

    public void ChooseWeapon(int chosenWeapon)
    {
        StopAllCoroutines();

        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }

        weapons[chosenWeapon].SetActive(true);
    }
}