using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SyphonSenderFinder : MonoBehaviour
{

    public bool autoConnect;
    public string senderName;
    public Vector2 resolution;
    public SyphonReceiver syphonReceiver;
    public RawImage rawImage;

    private RenderTexture renterTextrue;

    private void Awake()
    {
        renterTextrue = new RenderTexture((int)resolution.x, (int)resolution.y, 16, RenderTextureFormat.ARGB32);
        renterTextrue.name = "receiver_rt";
        syphonReceiver.targetTexture = renterTextrue;
        rawImage.texture = renterTextrue;
    }

    private void Update()
    {
        if (!autoConnect) return;
        if (syphonReceiver.senderName != senderName)
        {
            FindSender();
        }
    }

    void FindSender()
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
                syphonReceiver.senderName = _senderName;
            }
        }
    }

    #region Native plugin entry points

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