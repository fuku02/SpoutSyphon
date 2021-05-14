using System.Collections;
using UnityEngine;

public class SetFrameRate : MonoBehaviour
{

    [SerializeField]
    private int _targetFrameRate = 60;
    private float currentFps;
    private bool isDebugMode = false;
    private GUIStyle style;
    private GUIStyleState styleState;

    void Start()
    {
        Apply();

        style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;

        styleState = new GUIStyleState();
        styleState.textColor = Color.white;
    }

    void OnValidate()
    {
        Apply();
    }

    void Apply()
    {
        Application.targetFrameRate = _targetFrameRate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) isDebugMode = !isDebugMode;
        currentFps = 1.0f / Time.deltaTime;
    }

    void OnGUI()
    {
        if (!isDebugMode) return;
        GUISetSize(1024, 768);

        var label = string.Format("fps:{0:00.0}", (currentFps));
        style.normal = styleState;
        GUI.Label(new Rect(10, 10, 1000, 1000), label, style);
    }

    private void GUISetSize(float screenWidth, float screenHeight)
    {
        float horizRatio = Screen.width / screenWidth;
        float vertRatio = Screen.height / screenHeight;
        float ratio = Mathf.Max(horizRatio, vertRatio);
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ratio, ratio, 1));
    }

}