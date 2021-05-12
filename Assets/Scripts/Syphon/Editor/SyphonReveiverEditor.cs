using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SyphonReceiver))]
public class SyphonReceiverEditor : Editor
{
    SerializedProperty _appName;
    SerializedProperty _senderName;
    SerializedProperty _targetTexture;

    // static GUIContent _labelProperty = new GUIContent("Property");

    // string[] _propertyList; // Cached property list
    // Shader _cachedShader;   // Shader stored in the cache

    // // Retrieve the shader from the target renderer.
    // Shader RetrieveTargetShader(UnityEngine.Object target)
    // {
    //     var renderer = target as Renderer;
    //     if (renderer == null) return null;

    //     var material = renderer.sharedMaterial;
    //     if (material == null) return null;

    //     return material.shader;
    // }

    // // Cache the properties of the given shader .
    // void CachePropertyList(Shader shader)
    // {
    //     // Do nothing if the shader is same to the cached one.
    //     if (_cachedShader == shader) return;

    //     var temp = new List<string>();

    //     var count = ShaderUtil.GetPropertyCount(shader);
    //     for (var i = 0; i < count; i++)
    //     {
    //         var propType = ShaderUtil.GetPropertyType(shader, i);
    //         if (propType == ShaderUtil.ShaderPropertyType.TexEnv)
    //             temp.Add(ShaderUtil.GetPropertyName(shader, i));
    //     }

    //     _propertyList = temp.ToArray();
    //     _cachedShader = shader;
    // }

    void OnEnable()
    {
        _appName = serializedObject.FindProperty("_appName");
        _senderName = serializedObject.FindProperty("_senderName");
        _targetTexture = serializedObject.FindProperty("_targetTexture");
    }

    // void OnDisable()
    // {
    //     _propertyList = null;
    //     _cachedShader = null;
    // }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        if (EditorApplication.isPlaying)
        {

            EditorGUILayout.DelayedTextField(_appName);
            EditorGUILayout.DelayedTextField(_senderName);

            // Force reconnection on modification to name properties.
            if (EditorGUI.EndChangeCheck())
                foreach (MonoBehaviour client in targets)
                    client.SendMessage("OnDisable");

            EditorGUILayout.PropertyField(_targetTexture);
        }

        serializedObject.ApplyModifiedProperties();
    }
}