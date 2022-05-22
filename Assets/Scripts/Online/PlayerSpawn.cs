using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    public GameObject player;

    public Transform spawnPoint;

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
}
