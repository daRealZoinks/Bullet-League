using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    public GameObject player;

    public List<Transform> blueTeamSpawn;
    public List<Transform> orangeTeamSpawn;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            Spawn();
        }
        else
        {
            Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void Spawn()
    {
        Transform spawnPoint;

        if (PhotonNetwork) //if blue team
        {
            spawnPoint = blueTeamSpawn[Random.Range(0, orangeTeamSpawn.Count)];
        }
        else
        {
            spawnPoint = orangeTeamSpawn[Random.Range(0, orangeTeamSpawn.Count)];
        }

        PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation);
    }
}