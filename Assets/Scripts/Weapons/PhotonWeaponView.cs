using Photon.Pun;
using UnityEngine;

public class PhotonWeaponView : MonoBehaviour, IPunObservable
{
    private WeaponDispenser weaponDispenser;

    private void Start()
    {
        weaponDispenser = GetComponent<WeaponDispenser>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(weaponDispenser.ready);
            stream.SendNext(weaponDispenser.activeWeaponNumber);
        }
        else
        {
            weaponDispenser.ready = (bool)stream.ReceiveNext();
            weaponDispenser.activeWeaponNumber = (int)stream.ReceiveNext();

            weaponDispenser.weapons[weaponDispenser.activeWeaponNumber].SetActive(weaponDispenser.ready);
        }
    }

}
