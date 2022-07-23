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
        bool[] active = new bool[gunManager.weapons.Length];

        for (int i = 0; i < gunManager.weapons.Length; i++)
        {
            active[i] = gunManager.weapons[i].activeSelf;
        }

        if (stream.IsWriting)
        {
            for (int i = 0; i < gunManager.weapons.Length; i++)
            {
                stream.SendNext(active[i]);
            }
        }
        else
        {
            for (int i = 0; i < gunManager.weapons.Length; i++)
            {
                active[i] = (bool)stream.ReceiveNext();
                gunManager.weapons[i].SetActive(active[i]);
            }
        }
    }
}