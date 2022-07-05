using Photon.Pun;
using UnityEngine;

public class PlayerStart : MonoBehaviourPunCallbacks
{
    private bool canSpawn;
    private Vector3 vector = new(0, .5f, 0);

    private GameManager gameManager;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position - vector, transform.position + vector, .5f);
        if (colliders.Length > 0)
        {
            Gizmos.color = Color.red;
            canSpawn = false;
        }
        else
        {
            Gizmos.color = Color.green;
            canSpawn = true;
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Spawn()
    {
        if (canSpawn)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Instantiate(gameManager.playerPrefab.name, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(gameManager.playerPrefab, transform.position, transform.rotation);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(0, 1, 0), new Vector3(2, .5f, .5f));
    }
}
