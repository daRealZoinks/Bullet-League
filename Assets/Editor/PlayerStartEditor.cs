using UnityEditor;

[CustomEditor(typeof(PlayerStart))]
public class PlayerStartEditor : Editor
{
    private SerializedProperty team;

    private void OnEnable()
    {
        team = serializedObject.FindProperty("team");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var playerStart = target as PlayerStart;

        EditorGUILayout.PropertyField(team);
        playerStart.SetColor(playerStart.team);
        
        serializedObject.ApplyModifiedProperties();
    }
}