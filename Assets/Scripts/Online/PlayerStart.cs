using UnityEngine;
using Photon.Pun;

public class PlayerStart : MonoBehaviourPunCallbacks
{
    public Team team;

    private Color color;

    public Mesh mesh;

    private GameManager gameManager;

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    [ExecuteInEditMode]
    public void SetColor(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                color = new Color(23, 118, 227);
                break;
            case Team.Orange:
                color = new Color(227, 139, 23);
                break;
        }
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