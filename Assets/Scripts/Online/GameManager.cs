using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Ball")]
    private GameObject currentBall;
    public GameObject ballPrefab;

    [Header("Score")]
    public int blueScore;
    public int orangeScore;

    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI orangeScoreText;

    [Header("Players")]
    public GameObject playerPrefab;

    public List<PlayerStart> blueTeamPlayerSpawnPoints;
    public List<PlayerStart> orangeTeamPlayerSpawnPoints;

    public void Start()
    {
        foreach (PlayerStart playerStart in blueTeamPlayerSpawnPoints)
        {
            playerStart.SetGameManager(this);
        }

        foreach (PlayerStart playerStart in orangeTeamPlayerSpawnPoints)
        {
            playerStart.SetGameManager(this);
        }

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

        currentBall.GetComponent<Ball>().SetGameManager(this);

        blueScoreText.text = blueScore.ToString();
        orangeScoreText.text = orangeScore.ToString();
    }

    public void BlueScored()
    {
        blueScore++;
        RespawnBall();
    }

    public void OrangeScored()
    {
        orangeScore++;
        RespawnBall();
    }

    private void RespawnBall()
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