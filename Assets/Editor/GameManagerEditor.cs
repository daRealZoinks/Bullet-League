using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    SerializedProperty ballPrefab;

    SerializedProperty blueExplosion;
    SerializedProperty orangeExplosion;

    SerializedProperty playerPrefab;

    SerializedProperty blueTeamPlayerSpawnPoints;
    SerializedProperty orangeTeamPlayerSpawnPoints;

    SerializedProperty offlinePlayerStart;

    private void OnEnable()
    {
        ballPrefab = serializedObject.FindProperty("ballPrefab");
        blueExplosion = serializedObject.FindProperty("blueExplosion");
        orangeExplosion = serializedObject.FindProperty("orangeExplosion");
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

        EditorGUILayout.PropertyField(blueExplosion);
        EditorGUILayout.PropertyField(orangeExplosion);
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