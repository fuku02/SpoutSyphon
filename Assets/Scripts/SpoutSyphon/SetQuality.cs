using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetQuality : MonoBehaviour
{
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

    bool isDebugMode = false;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        QualitySettings.SetQualityLevel((int)quality, true);
        // CreateComboBox();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) isDebugMode = !isDebugMode;
    }


    /*
        private ComboBox _comboBox;
        void CreateComboBox()
        {
            // Make a GUIStyle that has a solid white hover/onHover background to indicate highlighted items
            GUIStyle listStyle = new GUIStyle();
            listStyle.normal.textColor = Color.white;
            listStyle.onHover.background =
            listStyle.hover.background = new Texture2D(2, 2);
            listStyle.padding.left =
            listStyle.padding.right =
            listStyle.padding.top =
            listStyle.padding.bottom = 6;

            var list = Enum.GetNames(typeof(Quality)).Cast<string>().ToList();
            _comboBox = new ComboBox(new Rect(10, 60, 220, 30), list, listStyle);
        }

        // Update is called once per frame
        void OnGUI()
        {
            if (!isDebugMode) return;

            GUISetSize(1024, 768);

            string previous = _comboBox.SelectedItem;
            _comboBox.Show();
            GUI.Label(new Rect(_comboBox.rect.x, _comboBox.rect.y - 20, 400, 20), "Quality :");

            if (_comboBox.SelectedItem != previous)
            {
                QualitySettings.SetQualityLevel((int)_comboBox.SelectedItemIndex, true);
            }
        }

        private void GUISetSize(float screenWidth, float screenHeight)
        {
            float horizRatio = Screen.width / screenWidth;
            float vertRatio = Screen.height / screenHeight;
            float ratio = Mathf.Max(horizRatio, vertRatio);
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ratio, ratio, 1));
        }
        */
}
