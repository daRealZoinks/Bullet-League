using Photon.Pun;
using UnityEngine;

public class PhotonMatchView : MonoBehaviour, IPunObservable
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameManager.blueScore);
            stream.SendNext(gameManager.orangeScore);
        }
        else
        {
            gameManager.blueScore = (int)stream.ReceiveNext();
            gameManager.orangeScore = (int)stream.ReceiveNext();
        }
    }
}
