/************************************************************
■参考
	Github
		https://github.com/SJ-magic-study-unity/study__UnityFixFramerate
		
	UnityでFPSを設定する方法
		http://unityleaning.blog.fc2.com/blog-entry-2.html
************************************************************/
using UnityEngine;
using System.Collections;

/************************************************************
************************************************************/
public class SetFrameRate : MonoBehaviour
{

    private string label = "";
    KeyCode Key_Disp = KeyCode.R;
    bool b_Disp = false;

    [SerializeField] int FrameRate = 60;

    void Awake()
    {
        //QualitySettings.vSyncCount = 1; // Don't Sync
        Application.targetFrameRate = FrameRate;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(Key_Disp)) b_Disp = !b_Disp;

        float fps = 1.0f / Time.deltaTime;
        label = string.Format("{0:000.0}", (int)(fps + 0.5f));
    }

    /****************************************
	****************************************/
    void OnGUI()
    {
        GUI.color = Color.white;

        /********************
		********************/
        if (b_Disp) GUI.Label(new Rect(15, 100, 500, 50), label);
    }
}
