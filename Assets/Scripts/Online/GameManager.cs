using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject currentBall;
    public GameObject ballPrefab;

    public int blueScore;
    public int orangeScore;

    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI orangeScoreText;

    public void Start()
    {
        SpawnBall();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log(newMasterClient.NickName + " is now the masta'");

        PhotonView currentBallView = currentBall.GetPhotonView();

        currentBallView.TransferOwnership(newMasterClient);

        base.OnMasterClientSwitched(newMasterClient);
    }

    public void SpawnBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentBall = PhotonNetwork.Instantiate(ballPrefab.name, transform.position, transform.rotation);
        }

        if (PhotonNetwork.OfflineMode)
        {
            currentBall = Instantiate(ballPrefab, transform.position, transform.rotation);
        }

        blueScoreText.text = blueScore.ToString();
        orangeScoreText.text = orangeScore.ToString();
    }

    public void BlueScored()
    {
        blueScore++;
        Respawn();
    }

    public void OrangeScored()
    {
        orangeScore++;
        Respawn();
    }

    private void Respawn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(currentBall);
        }

        if (PhotonNetwork.OfflineMode)
        {
            Destroy(currentBall);
        }

        SpawnBall();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(blueScore);
            stream.SendNext(orangeScore);
        }
        else
        {
            blueScore = (int)stream.ReceiveNext();
            orangeScore = (int)stream.ReceiveNext();

            blueScoreText.text = blueScore.ToString();
            orangeScoreText.text = orangeScore.ToString();
        }
    }
}