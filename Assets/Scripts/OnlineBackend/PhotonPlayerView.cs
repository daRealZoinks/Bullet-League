using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayerView : MonoBehaviour,IPunObservable
{
    private PlayerManager _playerManager;

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
