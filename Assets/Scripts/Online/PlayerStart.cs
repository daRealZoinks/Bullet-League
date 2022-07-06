using Photon.Pun;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerStart : MonoBehaviourPunCallbacks
{
    public Color color;

    private GameManager gameManager;

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Spawn()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position + transform.up * transform.localScale.y / 2, transform.localScale);
        Gizmos.DrawLine(transform.position + transform.up * transform.localScale.y / 2, transform.position + transform.up * transform.localScale.y / 2 + transform.forward * 1.5f);
    }
}
