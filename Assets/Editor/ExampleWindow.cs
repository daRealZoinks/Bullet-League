using UnityEditor;
using UnityEngine;

public class ExampleWindow : EditorWindow
{
    public GameObject ball;
    public GameObject playerSpawnPrefab;
    public GameObject blueGoalPrefab;
    public GameObject orangeGoalPrefab;
    public GameObject UI;

    [MenuItem("Window/Prototype")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ExampleWindow>("Simple gameObjects");
    }

    void OnGUI()
    {
        GUILayout.Label("drag any of these into the editor", EditorStyles.boldLabel);

        EditorGUILayout.ObjectField(ball, typeof(GameObject), true);
        EditorGUILayout.ObjectField(playerSpawnPrefab, typeof(GameObject), true);
        EditorGUILayout.ObjectField(orangeGoalPrefab, typeof(GameObject), true);
        EditorGUILayout.ObjectField(blueGoalPrefab, typeof(GameObject), true);
        EditorGUILayout.ObjectField(UI, typeof(GameObject), true);
    }
}