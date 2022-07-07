using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private GameObject currentBall;
    public GameObject ballPrefab;

    public int blueScore;
    public int orangeScore;

    public GameObject playerPrefab;

    public UI ui;

    public bool online;

    public List<PlayerStart> blueTeamPlayerSpawnPoints;
    public List<PlayerStart> orangeTeamPlayerSpawnPoints;

    public PlayerStart offlinePlayerStart;

    private void Awake()
    {
        if (online)
        {
            foreach (PlayerStart playerStart in blueTeamPlayerSpawnPoints)
            {
                playerStart.SetGameManager(this);
            }

            foreach (PlayerStart playerStart in orangeTeamPlayerSpawnPoints)
            {
                playerStart.SetGameManager(this);
            }

            //handle the spawn of the whole playerbase later
        }
        else
        {
            PhotonNetwork.Disconnect();

            offlinePlayerStart.SetGameManager(this);

            offlinePlayerStart.Spawn();
        }

        if (ballPrefab != null && currentBall == null)
        {
            SpawnBall();
        }

        if (ui == null)
        {
            Instantiate(ui);
            ui.SetGameManager(this);
        }
    }

    [ExecuteInEditMode]
    public void Update()
    {
        foreach (PlayerStart playerStart in blueTeamPlayerSpawnPoints)
        {
            playerStart.SetColor(Team.Blue);
        }

        foreach (PlayerStart playerStart in orangeTeamPlayerSpawnPoints)
        {
            playerStart.SetColor(Team.Orange);
        }
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
    }

    public void BlueScored()
    {
        blueScore++;
        ui.UpdateScore();
        RespawnBall();
    }

    public void OrangeScored()
    {
        orangeScore++;
        ui.UpdateScore();
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
        }
    }
}
