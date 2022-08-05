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
        EditorGUILayout.PropertyField(TeamColor);

        switch (goal.teamColor)
        {
            case Team.Blue:
                goal.backGoal.GetComponent<MeshRenderer>().sharedMaterial = goal.blue;
                goal.goalField.GetComponent<MeshRenderer>().sharedMaterial = goal.blueTransparent;
                break;
            case Team.Orange:
                goal.backGoal.GetComponent<MeshRenderer>().sharedMaterial = goal.red;
                goal.goalField.GetComponent<MeshRenderer>().sharedMaterial = goal.redTransparent;
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}