using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SyphonSender))]
public class SyphonSenderEditor : Editor
{
    SerializedProperty _senderName;
    SerializedProperty _sourceTexture;
    SerializedProperty _alphaSupport;

    void OnEnable()
    {
        _senderName = serializedObject.FindProperty("_senderName");
        _sourceTexture = serializedObject.FindProperty("_sourceTexture");
        _alphaSupport = serializedObject.FindProperty("_alphaSupport");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.DelayedTextField(_senderName);

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