using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private GameObject currentBall;
    public GameObject ballPrefab;

    public int blueScore;
    public int orangeScore;

    public GameObject playerPrefab;

    public bool online;

    public List<PlayerStart> blueTeamPlayerSpawnPoints;
    public List<PlayerStart> orangeTeamPlayerSpawnPoints;

    public PlayerStart offlinePlayerStart;

    public void Awake()
    {
        if (online)
        {
            foreach (PlayerStart playerStart in blueTeamPlayerSpawnPoints)
            {
                playerStart.SetGameManager(this);
            }

            foreach (PlayerStart playerStart in orangeTeamPlayerSpawnPoints)
            {
                playerStart.SetGameManager(this);
            }

            //handle the spawn of the whole playerbase later
        }
        else
        {
            PhotonNetwork.OfflineMode = true;

            offlinePlayerStart.SetGameManager(this);

            offlinePlayerStart.Spawn();
        }

        if (ballPrefab != null)
        {
            SpawnBall();
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log(newMasterClient.NickName + " is now the masta'");

        PhotonView currentBallView = currentBall.GetPhotonView();

        currentBallView.TransferOwnership(newMasterClient);

        base.OnMasterClientSwitched(newMasterClient);
    }

    public void SpawnBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentBall = PhotonNetwork.Instantiate(ballPrefab.name, transform.position, transform.rotation);
        }

        if (PhotonNetwork.OfflineMode)
        {
            currentBall = Instantiate(ballPrefab, transform.position, transform.rotation);
        }

        currentBall.GetComponent<Ball>().SetGameManager(this);
    }

    public void BlueScored()
    {
        blueScore++;
        RespawnBall();
    }

    public void OrangeScored()
    {
        orangeScore++;
        RespawnBall();
    }

    private void RespawnBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(currentBall);
        }

        if (PhotonNetwork.OfflineMode)
        {
            Destroy(currentBall);
        }

        SpawnBall();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(blueScore);
            stream.SendNext(orangeScore);
        }
        else
        {
            blueScore = (int)stream.ReceiveNext();
            orangeScore = (int)stream.ReceiveNext();
        }
    }
}

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    SerializedProperty ballPrefab;

    SerializedProperty playerPrefab;

    SerializedProperty blueTeamPlayerSpawnPoints;
    SerializedProperty orangeTeamPlayerSpawnPoints;

    SerializedProperty offlinePlayerStart;

    private void OnEnable()
    {
        ballPrefab = serializedObject.FindProperty("ballPrefab");
        playerPrefab = serializedObject.FindProperty("playerPrefab");
        blueTeamPlayerSpawnPoints = serializedObject.FindProperty("blueTeamPlayerSpawnPoints");
        orangeTeamPlayerSpawnPoints = serializedObject.FindProperty("orangeTeamPlayerSpawnPoints");
        offlinePlayerStart = serializedObject.FindProperty("offlinePlayerStart");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var gameManager = target as GameManager;

        EditorGUILayout.PropertyField(ballPrefab);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(playerPrefab);
        EditorGUILayout.Space();

        gameManager.online = GUILayout.Toggle(gameManager.online, "Online");
        EditorGUILayout.Space();

        if (gameManager.online)
        {
            EditorGUILayout.PropertyField(blueTeamPlayerSpawnPoints);
            EditorGUILayout.PropertyField(orangeTeamPlayerSpawnPoints);
        }
        else
        {
            EditorGUILayout.PropertyField(offlinePlayerStart);
        }

        serializedObject.ApplyModifiedProperties();
    }
}