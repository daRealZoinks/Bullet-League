using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject[] weapons;

    public GunManager gunManager;

    public void ChooseWeapon(int weapon)
    {
        StopAllCoroutines();
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[weapon].SetActive(true);
    }
}
