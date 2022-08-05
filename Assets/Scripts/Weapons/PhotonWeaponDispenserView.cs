using Photon.Pun;
using UnityEngine;

public class PhotonWeaponDispenserView : MonoBehaviour, IPunObservable
{
    private WeaponDispenser _weaponDispenser;

    private void Awake()
    {
        _weaponDispenser = GetComponent<WeaponDispenser>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.Serialize(ref _weaponDispenser.ready);
            stream.Serialize(ref _weaponDispenser.activeWeaponIndex);
        }
        else
        {
            stream.Serialize(ref _weaponDispenser.ready);
            stream.Serialize(ref _weaponDispenser.activeWeaponIndex);

            _weaponDispenser.particles.SetActive(_weaponDispenser.ready);

            foreach (var weapon in _weaponDispenser.weapons)
            {
                weapon.SetActive(false);
            }

            _weaponDispenser.weapons[_weaponDispenser.activeWeaponIndex].SetActive(_weaponDispenser.ready);
        }
    }
}