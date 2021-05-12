using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class SpoutSyphonSender : MonoBehaviour
{
    public string senderName;
    public bool alphaSupport;
    private SpoutSender spoutSender;
    private SyphonServer syphonSender;

    private void Awake()
    {

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // Spout
        // senderNameを「appName / senderName」に整形
        gameObject.name = Application.productName + "/" + senderName;
        CheckSpoutSender();
        spoutSender = gameObject.AddComponent<SpoutSender>();
        spoutSender.alphaSupport = alphaSupport;
#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        // Syphon
        // senderNameは、自動で「appName / senderName」になる
        gameObject.name = senderName;
        syphonSender = gameObject.AddComponent<SyphonServer>();
        syphonSender.alphaSupport = alphaSupport;
#endif
    }

    private void Update()
    { }

    void CheckSpoutSender()
    {
        var count = SpoutPluginEntry.ScanSharedObjects();
        for (var i = 0; i < count; i++)
        {
            var _senderName = SpoutPluginEntry.GetSharedObjectNameString(i);
            if (_senderName == Application.productName + "/" + senderName)
            {
                Debug.LogError(String.Format("- {0} is already being used by other sender. ", _senderName));
            }
        }
    }

    void CheckSyphonSender()
    {
        var list = SyphonPluginEntry.Plugin_CreateServerList();
        var count = SyphonPluginEntry.Plugin_GetServerListCount(list);
        for (var i = 0; i < count; i++)
        {
            var pSenderName = SyphonPluginEntry.Plugin_GetNameFromServerList(list, i);
            var pAppName = SyphonPluginEntry.Plugin_GetAppNameFromServerList(list, i);
            var _senderName = (pSenderName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pSenderName) : "(no sender name)";
            var _appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "(no app name)";

            if (_senderName == senderName && _appName == (Application.platform == RuntimePlatform.OSXEditor? "unity": Application.productName))
            {
                Debug.LogError(String.Format("- {0}/{1} is already being used by other sender. ", _appName, _senderName));
            }
        }
    }
}