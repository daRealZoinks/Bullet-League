using UnityEngine;
using Photon.Pun;

public class PhotonGunManagerView : MonoBehaviour, IPunObservable
{
    private GunManager gunManager;

    private void Awake()
    {
        gunManager = GetComponent<GunManager>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gunManager.activeGun);

            foreach (var weapon in gunManager.weapons)
            {
                stream.SendNext(weapon.activeSelf);
            }
        }
        else
        {
            gunManager.activeGun = (Gun)stream.ReceiveNext();

            foreach (var weapon in gunManager.weapons)
            {
                weapon.SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}