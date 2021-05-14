using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine; // need this to declare SyphonServer

public class SetResolution : MonoBehaviour
{
    [SerializeField]
    private Vector2 _resolution = new Vector2(1280, 720);

    void Awake()
    {
        Apply();
    }

    void OnValidate()
    {
        Apply();
    }

    void Apply()
    {
        Screen.SetResolution((int)_resolution.x, (int)_resolution.y, false);
        StartCoroutine(CheckSetResolution(_resolution));
    }

    IEnumerator CheckSetResolution(Vector2 targetResolution)
    {
        yield return new WaitUntil(() => Screen.width == (int)targetResolution.x && Screen.height == (int)targetResolution.y);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        SpoutSender[] spoutSenderList = FindObjectsOfType<SpoutSender>();
        foreach (var sender in spoutSenderList)
        {
            sender.enabled = false;
            sender.enabled = true;
        }
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        SyphonClient[] syphonSenderList = FindObjectsOfType<SyphonClient>();
        foreach (var sender in syphonSenderList)
        {
            sender.enabled = false;
            sender.enabled = true;
        }
#endif
    }

    bool isDebugMode = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) isDebugMode = !isDebugMode;
    }

}