using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
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
            Team.Blue => new Color(23 / 255f, 118, 227 / 255f),
            Team.Orange => new Color(227 / 255f, 139 / 255f, 23 / 255f),
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
               (playerStartTransform = transform).position -
               playerStartTransform.up * playerStartTransform.localScale.y / 2, playerStartTransform.rotation);
        }
        
        player.GetComponent<PlayerManager>().Team = team;

        switch (team)
        {
            default:
            case Team.Blue:
                PhotonNetwork.LocalPlayer.JoinTeam("Blue");
                break;
            case Team.Orange:
                PhotonNetwork.LocalPlayer.JoinTeam("Orange");
                break;
        }
        
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