using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;

    public GameObject blueExplosion;
    public GameObject orangeExplosion;

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

            if (PhotonNetwork.PlayerList.Length % 2 == 1)
            {
                blueTeamPlayerSpawnPoints[Random.Range(0, blueTeamPlayerSpawnPoints.Count - 1)].Spawn();
            }
            else
            {
                orangeTeamPlayerSpawnPoints[Random.Range(0, orangeTeamPlayerSpawnPoints.Count - 1)].Spawn();
            }
        }
        else
        {
            PhotonNetwork.Disconnect();

            offlinePlayerStart.SetGameManager(this);

            offlinePlayerStart.Spawn();
        }

        SpawnBall();

        if (ui == null)
        {
            Instantiate(ui);
        }
        ui.SetGameManager(this);
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
    }

    public void BlueScored()
    {
        blueScore++;
        ui.UpdateScore();
        Instantiate(orangeExplosion, currentBall.transform.position, Quaternion.identity);
        RespawnBall();
    }

    public void OrangeScored()
    {
        orangeScore++;
        ui.UpdateScore();
        Instantiate(blueExplosion, currentBall.transform.position, Quaternion.identity);
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
}