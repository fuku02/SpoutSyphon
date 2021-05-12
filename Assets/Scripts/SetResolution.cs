/************************************************************
■参考URL
	■How to wait for Screen.SetResolution to finish?
		https://answers.unity.com/questions/626543/how-to-wait-for-screensetresolution-to-finish.html
		
	■WaitUntil
		https://docs.unity3d.com/ja/current/ScriptReference/WaitUntil.html
	
************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Klak.Syphon; // need this to declare SyphonServer

/************************************************************
************************************************************/
public class SetResolution : MonoBehaviour
{
    enum IMAGE_QUALITY{
        VERY_LOW,
        LOW,
        MID,
        HIGH,
        VERY_HIGH,
        ULTRA,
    };
	
	[SerializeField] SyphonServer SyphonServer;
	readonly Vector2 resolution = new Vector2(1280, 720);
	
	bool b_ResolutionSet = false;
	
	enum STATE{
		WAIT__SET_RESOLUTION,
		COMP__SET_RESOLUTION,
	};
	STATE State = STATE.WAIT__SET_RESOLUTION;
    
	/******************************
	******************************/
    void Awake()
    {
		/********************
		Screen.SetResolution()は別threadで実行され、完了までに時間が掛かる(環境依存だが、1sec - )。
		********************/
		QualitySettings.SetQualityLevel((int)IMAGE_QUALITY.HIGH, true/* applyExpensiveChanges */);
		Screen.SetResolution((int)resolution.x, (int)resolution.y, false/* full screen */);
		
		/********************
		Coroutineを使って、SetResolutionの完了をjudge.
		********************/
		SyphonServer.enabled = false;
		StartCoroutine(_IsSetResolution_OK(resolution));
	}
	/******************************
	******************************/
    void Start()
    {
    }

	/******************************
	******************************/ 
    void Update()
    {
		/********************
		********************/
		switch(State){
			case STATE.WAIT__SET_RESOLUTION:
				if(b_ResolutionSet){
					State = STATE.COMP__SET_RESOLUTION;
					SyphonServer.enabled = true;
				}
				break;
				
			case STATE.COMP__SET_RESOLUTION:
				break;
		
		}
    }
	
	/****************************************
	****************************************/
	IEnumerator _IsSetResolution_OK(Vector2 targetResolution) {
		yield return new WaitUntil(() => Screen.width == (int)targetResolution.x && Screen.height == (int)targetResolution.y);
		
		b_ResolutionSet = true;
	}
}
