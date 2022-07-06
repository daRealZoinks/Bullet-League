using Photon.Pun;
using UnityEngine;

public class PlayerStart : MonoBehaviourPunCallbacks
{
    public Color color;

    private Mesh mesh;

    private GameManager gameManager;

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Spawn()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(gameManager.playerPrefab.name, transform.position - transform.up * transform.localScale.y / 2, transform.rotation);
        }
        else
        {
            Instantiate(gameManager.playerPrefab, transform.position - transform.up * transform.localScale.y / 2, transform.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        // Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1.5f);
        Gizmos.DrawRay(transform.position, transform.forward * 1.5f);

        Gizmos.color = new Color(color.r, color.g, color.b, 5f / 255);
        Gizmos.DrawWireMesh(mesh, -1, transform.position, transform.rotation, transform.localScale);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 30f / 255);
        Gizmos.DrawWireMesh(mesh, -1, transform.position, transform.rotation, transform.localScale);
    }
}
