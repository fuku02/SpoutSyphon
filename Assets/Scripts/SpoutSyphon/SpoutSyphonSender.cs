using System.Collections;
using System.Collections.Generic;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SpoutSyphonSender : MonoBehaviour
{
    public string senderName;
    public Vector2 resolution = new Vector2(1920, 1080);
    public bool alphaSupport;
    private SpoutSender spoutSender;
    private SyphonServer syphonSender;
    private RenderTexture renderTextrue;

    private void Awake()
    {
        gameObject.name = senderName;
        renderTextrue = new RenderTexture((int) resolution.x, (int) resolution.y, 16, RenderTextureFormat.Default);
        renderTextrue.name = "rt_" + senderName;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        spoutSender = gameObject.AddComponent<SpoutSender>();
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        syphonSender = gameObject.AddComponent<SyphonServer>();
#endif

        if (spoutSender != null)
        {
            spoutSender.sourceTexture = renderTextrue;
            spoutSender.alphaSupport = alphaSupport;
        }

        if (syphonSender != null)
        {
            syphonSender.sourceTexture = renderTextrue;
            syphonSender.alphaSupport = alphaSupport;
        }
    }
}