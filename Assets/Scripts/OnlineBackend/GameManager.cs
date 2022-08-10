using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab;
    private GameObject _currentBall;

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

    public bool online = true;

    [Space]

    public List<PlayerStart> blueTeamPlayerSpawnPoints;
    public List<PlayerStart> orangeTeamPlayerSpawnPoints;

    [Space]

    public PlayerStart offlinePlayerStart;

    private void Awake()
    {
        if (online)
        {
            foreach (var playerStart in blueTeamPlayerSpawnPoints)
            {
                playerStart.GameManager = this;
            }

            foreach (var playerStart in orangeTeamPlayerSpawnPoints)
            {
                playerStart.GameManager = this;
            }

            if (PhotonNetwork.PlayerList.Length % 2 == 1)
            {
                var playerStart = blueTeamPlayerSpawnPoints[Random.Range(0, blueTeamPlayerSpawnPoints.Count)];
                playerStart.Spawn();
            }
            else
            {
                var playerStart = orangeTeamPlayerSpawnPoints[Random.Range(0, orangeTeamPlayerSpawnPoints.Count)];
                playerStart.Spawn();
            }
        }
        else
        {
            PhotonNetwork.Disconnect();

            offlinePlayerStart.GameManager = this;

            offlinePlayerStart.Spawn();
        }

        SpawnBall();

        if (ui == null)
        {
            Instantiate(ui);
        }

        ui.SetGameManager(this);
    }

    private void SpawnBall()
    {
        var playerTransform = transform;

        if (PhotonNetwork.IsMasterClient)
        {
            _currentBall = PhotonNetwork.Instantiate(ballPrefab.name, playerTransform.position, playerTransform.rotation);
        }

        if (PhotonNetwork.OfflineMode)
        {
            _currentBall = Instantiate(ballPrefab, playerTransform.position, playerTransform.rotation);
        }
    }

    public void BlueScored()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            blueScore++;
            PhotonNetwork.Instantiate(orangeExplosion.name, _currentBall.transform.position, Quaternion.identity);
        }
        ui.UpdateScore();

        RespawnBall();
    }

    public void OrangeScored()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            orangeScore++;
            PhotonNetwork.Instantiate(blueExplosion.name, _currentBall.transform.position, Quaternion.identity);
        }
        ui.UpdateScore();

        RespawnBall();
    }

    private void RespawnBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(_currentBall);
        }

        if (PhotonNetwork.OfflineMode)
        {
            Destroy(_currentBall);
        }

        SpawnBall();
    }
}