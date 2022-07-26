using Photon.Pun;
using UnityEngine;

public class PhotonWeaponDispenserView : MonoBehaviour, IPunObservable
{
    private WeaponDispenser weaponDispenser;

    private void Awake()
    {
        weaponDispenser = GetComponent<WeaponDispenser>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
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

            for (int i = 0; i < weaponDispenser.weapons.Length; i++)
            {
                weaponDispenser.weapons[i].SetActive(i == weaponDispenser.activeWeaponIndex);
            }
        }
    }
}