using Photon.Pun;
using UnityEngine;

public class PlayerStart : MonoBehaviourPunCallbacks
{
    public Team team;
    public Mesh mesh;
    private Color _color;
    public GameManager GameManager { get; set; }

    [ExecuteInEditMode]
    public void SetColor(Team teamValue)
    {
        _color = teamValue switch
        {
            Team.Blue => new Color(23.0f / 255.0f, 118.0f/ 255.0f, 227.0f / 255.0f),
            Team.Orange => new Color(227.0f / 255.0f, 139.0f / 255.0f, 23.0f / 255.0f),
            _ => _color
        };
    }

    public GameObject Spawn()
    {
        var playerStartTransform = transform;

        GameObject player;

        if (PhotonNetwork.IsConnected)
        {
            player = PhotonNetwork.Instantiate(GameManager.playerPrefab.name,
                playerStartTransform.position - playerStartTransform.up * playerStartTransform.localScale.y / 2,
                playerStartTransform.rotation);
        }
        else
        {
            player = Instantiate(GameManager.playerPrefab,
               playerStartTransform.position - playerStartTransform.up * playerStartTransform.localScale.y / 2,
               playerStartTransform.rotation);
        }
        
        player.GetComponent<PlayerManager>().Team = team;

        return player;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var playerStartTransform = transform;
        var position = playerStartTransform.position;

        Gizmos.DrawRay(position, playerStartTransform.forward * 1.5f);

        Gizmos.color = new Color(_color.r, _color.g, _color.b, 10f / 255);
        Gizmos.DrawWireMesh(mesh, -1, position, playerStartTransform.rotation, playerStartTransform.localScale);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(_color.r, _color.g, _color.b, 55f / 255);
        var playerStartTransform = transform;
        Gizmos.DrawWireMesh(mesh, -1, playerStartTransform.position, playerStartTransform.rotation, playerStartTransform.localScale);
    }
}