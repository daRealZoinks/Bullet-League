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
            PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void Spawn()
    {
        if(PhotonNetwork)
        {

        }
    }
}