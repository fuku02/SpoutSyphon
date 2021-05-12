using System.Collections;
using UnityEngine;

public class SetFrameRate : MonoBehaviour
{

    [SerializeField] int targetFrameRate = 60;
    [SerializeField] float currentFps;
    [SerializeField] bool isStats = false;
    private GUIStyle style;
    private GUIStyleState styleState;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFrameRate;

        style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;

        styleState = new GUIStyleState();
        styleState.textColor = Color.white;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) isStats = !isStats;
        currentFps = 1.0f / Time.deltaTime;
    }

    void OnGUI()
    {
        if (!isStats) return;
        var label = string.Format("fps:{0:00.0}", (currentFps));
        style.normal = styleState;
        GUI.Label(new Rect(10, 10, 1000, 1000), label, style);
    }
}