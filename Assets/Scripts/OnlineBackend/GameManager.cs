using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;
    [Space]
    public GameObject blueExplosion;
    public GameObject orangeExplosion;
    [Space]
    public int blueScore;
    public int orangeScore;
    [Space]
    public GameObject playerPrefab;
    [Space]
    public UI ui;
    [Space]
    public bool online;
    [Space]
    public List<PlayerStart> blueTeamPlayerSpawnPoints;
    public List<PlayerStart> orangeTeamPlayerSpawnPoints;
    [Space]
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
        if (PhotonNetwork.IsMasterClient)
        {
            blueScore++;
        }
        ui.UpdateScore();
        Instantiate(orangeExplosion, currentBall.transform.position, Quaternion.identity);
        RespawnBall();
    }
    public void OrangeScored()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            orangeScore++;
        }
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