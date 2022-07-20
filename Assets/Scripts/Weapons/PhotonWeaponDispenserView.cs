using Photon.Pun;
using UnityEngine;

public class PhotonWeaponDispenserView : MonoBehaviour, IPunObservable
{
    private WeaponDispenser weaponDispenser;

    private void Start()
    {
        weaponDispenser = GetComponent<WeaponDispenser>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //TODO: fix weapon dispenser active weapon syncronisation

        if (stream.IsWriting)
        {
            stream.SendNext(weaponDispenser.ready);
            stream.SendNext(weaponDispenser.activeWeaponNumber);
        }
        else
        {
            weaponDispenser.ready = (bool)stream.ReceiveNext();
            weaponDispenser.activeWeaponNumber = (int)stream.ReceiveNext();
            weaponDispenser.particles.SetActive(weaponDispenser.ready);
        }

        for (int i = 0; i < weaponDispenser.weapons.Length; i++)
        {
            weaponDispenser.weapons[i].SetActive(i == weaponDispenser.activeWeaponNumber);
        }
    }
}