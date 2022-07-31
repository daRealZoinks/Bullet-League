using Photon.Pun;
using UnityEngine;

public class PlayerStart : MonoBehaviourPunCallbacks
{
    public Team team;
    public Mesh mesh;
    private Color color;
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
                color = new Color(23 / 255f, 118, 227 / 255f);
                break;
            case Team.Orange:
                color = new Color(227 / 255f, 139 / 255f, 23 / 255f);
                break;
        }
    }
    public GameObject Spawn()
    {
        if (PhotonNetwork.IsConnected)
        {
            return PhotonNetwork.Instantiate(gameManager.playerPrefab.name, transform.position - transform.up * transform.localScale.y / 2, transform.rotation);
        }
        else
        {
            return Instantiate(gameManager.playerPrefab, transform.position - transform.up * transform.localScale.y / 2, transform.rotation);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * 1.5f);

        Gizmos.color = new Color(color.r, color.g, color.b, 10f / 255);
        Gizmos.DrawWireMesh(mesh, -1, transform.position, transform.rotation, transform.localScale);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 55f / 255);
        Gizmos.DrawWireMesh(mesh, -1, transform.position, transform.rotation, transform.localScale);
    }
}