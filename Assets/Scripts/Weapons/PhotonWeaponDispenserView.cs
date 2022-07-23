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
        if (stream.IsWriting)
        {
            stream.SendNext(weaponDispenser.ready);
            stream.SendNext(weaponDispenser.activeWeaponIndex);
        }
        else
        {
            weaponDispenser.ready = (bool)stream.ReceiveNext();
            weaponDispenser.activeWeaponIndex = (int)stream.ReceiveNext();
            weaponDispenser.particles.SetActive(weaponDispenser.ready);

            weaponDispenser.weapons[weaponDispenser.activeWeaponIndex].SetActive(weaponDispenser.ready);
        }
    }
}