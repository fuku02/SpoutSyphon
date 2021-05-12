using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SyphonSender))]
public class SyphonSenderEditor : Editor
{
    SerializedProperty _sourceTexture;
    SerializedProperty _alphaSupport;

    void OnEnable()
    {
        _sourceTexture = serializedObject.FindProperty("_sourceTexture");
        _alphaSupport = serializedObject.FindProperty("_alphaSupport");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var server = (SyphonSender)target;
        var camera = server.GetComponent<Camera>();

        if (camera != null)
        {
            EditorGUILayout.HelpBox(
                "Syphon Server is running in camera capture mode.",
                MessageType.None
            );

            EditorGUILayout.PropertyField(_alphaSupport);
        }
        else
        {
            EditorGUILayout.HelpBox(
                "Syphon Server is running in render texture mode.",
                MessageType.None
            );

            EditorGUILayout.PropertyField(_sourceTexture);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
