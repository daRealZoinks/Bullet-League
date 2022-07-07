using UnityEditor;

[CustomEditor(typeof(PlayerStart))]
public class PlayerStartEditor : Editor
{
    private SerializedProperty team;
    private SerializedProperty mesh;

    private void OnEnable()
    {
        team = serializedObject.FindProperty("team");
        mesh = serializedObject.FindProperty("mesh");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var playerStart = target as PlayerStart;

        EditorGUILayout.PropertyField(team);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(mesh);
        EditorGUILayout.Space();

        playerStart.SetColor(playerStart.team);

        serializedObject.ApplyModifiedProperties();
    }
}