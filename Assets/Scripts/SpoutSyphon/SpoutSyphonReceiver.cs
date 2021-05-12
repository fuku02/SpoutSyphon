using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class SpoutSyphonReceiver : MonoBehaviour
{
    public bool autoConnect;
    public string senderName;
    public Vector2 resolution = new Vector2(1920, 1080);
    private RawImage rawImage;
    private SyphonClient syphonReceiver;
    private SpoutReceiver spoutReceiver;

    private RenderTexture renterTextrue;

    private void Awake()
    {
        renterTextrue = new RenderTexture((int)resolution.x, (int)resolution.y, 16, RenderTextureFormat.ARGB32);
        renterTextrue.name = "rt_" + senderName;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        spoutReceiver = gameObject.AddComponent<SpoutReceiver>();
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        syphonReceiver = gameObject.AddComponent<SyphonClient>();
#endif
        if (spoutReceiver != null) spoutReceiver.targetTexture = renterTextrue;
        if (syphonReceiver != null) syphonReceiver.targetTexture = renterTextrue;

        rawImage = GetComponent<RawImage>();
        rawImage.texture = renterTextrue;
    }

    private void Update()
    {
        if (!autoConnect) return;
        if (spoutReceiver != null)
        {
            if (spoutReceiver.sourceName == null)
            {
                FindSpoutSender();
            }
        }
        if (syphonReceiver != null)
        {
            if (syphonReceiver.serverName == null)
            {
                FindSyphonSender();
            }
        }
    }

    void FindSpoutSender()
    {
        var count = SpoutPluginEntry.ScanSharedObjects();
        if (count == 0)
            print("No sender found.");
        else
            print(count + " sender(s) found.");

        for (var i = 0; i < count; i++)
        {
            var _name = SpoutPluginEntry.GetSharedObjectNameString(i);
            var _nameList = _name.Split(':');
            var _senderName = _nameList[_nameList.Length - 1].Trim();
            print(String.Format("- {0}", _name));
            if (_senderName == senderName)
            {
                spoutReceiver.sourceName = _name;
            }
        }
    }

    void FindSyphonSender()
    {
        var list = SyphonPluginEntry.Plugin_CreateServerList();
        var count = SyphonPluginEntry.Plugin_GetServerListCount(list);

        if (count == 0)
            print("No sender found.");
        else
            print(count + " sender(s) found.");

        for (var i = 0; i < count; i++)
        {
            var pSenderName = SyphonPluginEntry.Plugin_GetNameFromServerList(list, i);
            var pAppName = SyphonPluginEntry.Plugin_GetAppNameFromServerList(list, i);
            var _senderName = (pSenderName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pSenderName) : "(no sender name)";
            var _appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "(no app name)";
            print(String.Format("- {0}:{1}", _appName, _senderName));
            if (_senderName == senderName)
            {
                syphonReceiver.appName = _appName;
                syphonReceiver.serverName = _senderName;
            }
        }
    }

}