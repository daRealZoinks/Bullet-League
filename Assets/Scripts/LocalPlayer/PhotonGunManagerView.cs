using UnityEngine;
using Photon.Pun;

public class PhotonGunManagerView : MonoBehaviour, IPunObservable
{
    private GunManager _gunManager;

    private void Awake()
    {
        _gunManager = GetComponent<GunManager>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_gunManager.activeGun);

            foreach (var weapon in _gunManager.weapons)
            {
                stream.SendNext(weapon.activeSelf);
            }
        }
        else
        {
            _gunManager.activeGun = (Gun)stream.ReceiveNext();

            foreach (var weapon in _gunManager.weapons)
            {
                weapon.SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}