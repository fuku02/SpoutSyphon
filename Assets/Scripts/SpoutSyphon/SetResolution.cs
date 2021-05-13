using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine; // need this to declare SyphonServer

public class SetResolution : MonoBehaviour
{
    public Vector2 resolution = new Vector2(1280, 720);


    void Awake()
    {
        Screen.SetResolution((int)resolution.x, (int)resolution.y, false);
        StartCoroutine(CheckSetResolution(resolution));
    }

    IEnumerator CheckSetResolution(Vector2 targetResolution)
    {
        yield return new WaitUntil(() => Screen.width == (int)targetResolution.x && Screen.height == (int)targetResolution.y);

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

    bool isDebugMode = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) isDebugMode = !isDebugMode;
    }

}