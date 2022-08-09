using UnityEngine;
using Photon.Pun;

public class PhotonPlayerManagerView : MonoBehaviourPun, IPunObservable
{
    private PlayerManager _playerManager;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.AmOwner)
        {
            stream.SendNext(_playerManager.Team);
            Debug.Log("Sending team: " + _playerManager.Team);
        }
        else
        {
            _playerManager.Team = (Team)stream.ReceiveNext();
            Debug.Log("Receiving team: " + _playerManager.Team);
        }
    }
}
