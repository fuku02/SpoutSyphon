using System.Collections;
using System.Collections.Generic;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine; // need this to declare SyphonServer

public class SetResolution : MonoBehaviour
{
    public Vector2 resolution = new Vector2(1280, 720);
    public Quality quality = Quality.VERY_HIGH;
    public enum Quality
    {
        VERY_LOW,
        LOW,
        MID,
        HIGH,
        VERY_HIGH,
        ULTRA,
    }

    void Awake()
    {
        QualitySettings.SetQualityLevel((int) quality, true);
        Screen.SetResolution((int) resolution.x, (int) resolution.y, false);
        StartCoroutine(CheckSetResolution(resolution));
    }

    IEnumerator CheckSetResolution(Vector2 targetResolution)
    {
        yield return new WaitUntil(() => Screen.width == (int) targetResolution.x && Screen.height == (int) targetResolution.y);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        SpoutSender spoutSender = FindObjectOfType<SpoutSender>();
        if (spoutSender != null)
        {
            spoutSender.enabled = false;
            spoutSender.enabled = true;
        }
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        SyphonClient syphonSender = FindObjectOfType<SyphonClient>();
        if (syphonSender != null)
        {
            syphonSender.enabled = false;
            syphonSender.enabled = true;
        }
#endif
    }

}