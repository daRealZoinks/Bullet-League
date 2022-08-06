using UnityEngine;
using Photon.Pun;

public class PhotonPlayerManagerView : MonoBehaviour, IPunObservable
{
    private PlayerManager _playerManager;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_playerManager.Team);
        }
        else
        {
            _playerManager.Team = (Team)stream.ReceiveNext();
        }
    }
}
