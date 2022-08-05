using Photon.Pun;
using UnityEngine;

public class PhotonGameManagerView : MonoBehaviour, IPunObservable
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_gameManager.blueScore);
            stream.SendNext(_gameManager.orangeScore);
        }
        else
        {
            _gameManager.blueScore = (int)stream.ReceiveNext();
            _gameManager.orangeScore = (int)stream.ReceiveNext();
        }
    }
}