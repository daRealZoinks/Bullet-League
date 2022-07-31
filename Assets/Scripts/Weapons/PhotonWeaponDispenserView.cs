using Photon.Pun;
using UnityEngine;

public class PhotonWeaponDispenserView : MonoBehaviour, IPunObservable
{
    [HideInInspector] public bool I_WANT_THE_FOR_METHOD = true;
    private WeaponDispenser weaponDispenser;
    private void Awake()
    {
        weaponDispenser = GetComponent<WeaponDispenser>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        TODO:
        -Syncronise:
            -weaponDispenser.particles - only if they are active or not
            -weaponDispenser.weapons - only which one is active at a any time
            -weaponDispenser.ready
            -weaponDispenser.activeWeaponIndex - this one should b synced only once at the begining of the game but who dafuk can do that
        */

        if (PhotonNetwork.IsMasterClient)
        {
            stream.Serialize(ref weaponDispenser.ready);
            stream.Serialize(ref weaponDispenser.activeWeaponIndex);
        }
        else
        {
            stream.Serialize(ref weaponDispenser.ready);
            stream.Serialize(ref weaponDispenser.activeWeaponIndex);

            weaponDispenser.particles.SetActive(weaponDispenser.ready);

#if I_WANT_THE_FOR_METHOD
            for (int i = 0; i < weaponDispenser.weapons.Length; i++)
            {
                weaponDispenser.weapons[i].SetActive(i == weaponDispenser.activeWeaponIndex);
            }
#else
            foreach (var weapon in weaponDispenser.weapons)
            {
                weapon.SetActive(false);
            }

            weaponDispenser.weapons[weaponDispenser.activeWeaponIndex].SetActive(true);
#endif
        }
    }
}