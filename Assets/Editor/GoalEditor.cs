using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Goal))]
public class GoalEditor : Editor
{
    private SerializedProperty gameManager;
    private SerializedProperty TeamColor;
    private void OnEnable()
    {
        gameManager = serializedObject.FindProperty("gameManager");
        TeamColor = serializedObject.FindProperty("TeamColor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var goal = target as Goal;

        EditorGUILayout.PropertyField(gameManager);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(TeamColor);

        switch (goal.TeamColor)
        {
            case Team.Blue:
                goal.coloredPart.GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
                break;
            case Team.Orange:
                goal.coloredPart.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}