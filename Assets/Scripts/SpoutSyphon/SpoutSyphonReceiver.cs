using System.Collections.Generic;
using System;
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
            if (spoutReceiver.sourceName != senderName)
            {
                FindSpoutSender();
            }
        }
        if (syphonReceiver != null)
        {
            if (syphonReceiver.serverName != senderName)
            {
                FindSyphonSender();
            }
        }
    }

    void FindSpoutSender()
    {
        var count = PluginEntry.ScanSharedObjects();
        if (count == 0)
            print("No sender found.");
        else
            print(count + " sender(s) found.");

        for (var i = 0; i < count; i++)
        {
            var _senderName = PluginEntry.GetSharedObjectNameString(i);
            print(String.Format("- {0}", _senderName));
            if (_senderName == senderName)
            {
                spoutReceiver.sourceName = _senderName;
            }
        }
    }

    void FindSyphonSender()
    {
        var list = Plugin_CreateServerList();
        var count = Plugin_GetServerListCount(list);

        if (count == 0)
            print("No sender found.");
        else
            print(count + " sender(s) found.");

        for (var i = 0; i < count; i++)
        {
            var pSenderName = Plugin_GetNameFromServerList(list, i);
            var pAppName = Plugin_GetAppNameFromServerList(list, i);
            var _senderName = (pSenderName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pSenderName) : "(no sender name)";
            var _appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "(no app name)";
            print(String.Format("- {0} / {1}", _appName, _senderName));
            if (_senderName == senderName)
            {
                syphonReceiver.appName = _appName;
                syphonReceiver.serverName = _senderName;
            }
        }
    }

    #region Native plugin entry points for Syphon

    [DllImport("KlakSyphon")]
    static extern IntPtr Plugin_CreateServerList();

    [DllImport("KlakSyphon")]
    static extern void Plugin_DestroyServerList(IntPtr list);

    [DllImport("KlakSyphon")]
    static extern int Plugin_GetServerListCount(IntPtr list);

    [DllImport("KlakSyphon")]
    static extern IntPtr Plugin_GetNameFromServerList(IntPtr list, int index);

    [DllImport("KlakSyphon")]
    static extern IntPtr Plugin_GetAppNameFromServerList(IntPtr list, int index);

    #endregion
}