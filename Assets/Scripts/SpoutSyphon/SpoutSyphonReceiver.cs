using System.Text;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class SpoutSyphonReceiver : MonoBehaviour
{
    public bool autoConnect = true;
    [SerializeField]
    private string senderName;
    [SerializeField]
    private Vector2 resolution = new Vector2(1920, 1080);
    private RawImage rawImage;
    private SyphonClient syphonReceiver;
    private SpoutReceiver spoutReceiver;
    private RenderTexture renterTextrue;
    private string targetSenderName = "";

    private void Start()
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

        //combox生成
        CreateComboBox();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) isDebugMode = !isDebugMode;

        //senderを探索し
        if (spoutReceiver != null) FindSpoutSender();
        if (syphonReceiver != null) FindSyphonSender();

        //senderを自動接続(後出し優先)
        if (autoConnect)
        {
            for (var i = senderInfoList.Count - 1; i >= 0; i--)
            {
                var senderInfo = senderInfoList[i];
                if (senderInfo.senderName == senderName)
                {
                    SetReceiverSource(senderInfo.fullName);
                    break;
                }
            }
        }
    }

    //SenderNameと一致するsenderを探す
    void FindSpoutSender()
    {
        var count = SpoutPluginEntry.ScanSharedObjects();
        if (senderInfoList.Count == count) return;

        //senderの数が変わっていれば、更新
        senderInfoList = new List<SenderInfo>();
        for (var i = 0; i < count; i++)
        {
            var senderInfo = new SenderInfo();
            senderInfo.fullName = SpoutPluginEntry.GetSharedObjectNameString(i);
            var _nameList = senderInfo.fullName.Split(':');
            senderInfo.senderName = _nameList[_nameList.Length - 1].Trim();
            // print(senderInfo.fullName));

            //senderInfoListを更新
            senderInfoList.Add(senderInfo);
        }
        //comboBox用のsenderNameListを更新
        senderNameList = senderInfoList.Select(info => info.fullName).ToList();
        _comboBox.list = senderNameList;
    }

    //SenderNameと一致するsenderを探す
    //Syphonは"appName:senderName"なので、appNameは判定しない
    void FindSyphonSender()
    {
        var list = SyphonPluginEntry.Plugin_CreateServerList();
        var count = SyphonPluginEntry.Plugin_GetServerListCount(list);
        if (senderInfoList.Count == count) return;

        //senderの数が変わっていれば、更新
        senderInfoList = new List<SenderInfo>();
        for (var i = 0; i < count; i++)
        {
            var pSenderName = SyphonPluginEntry.Plugin_GetNameFromServerList(list, i);
            var pAppName = SyphonPluginEntry.Plugin_GetAppNameFromServerList(list, i);

            var senderInfo = new SenderInfo();
            senderInfo.senderName = (pSenderName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pSenderName) : "(no sender name)";
            senderInfo.appName = (pAppName != IntPtr.Zero) ? Marshal.PtrToStringAnsi(pAppName) : "";
            senderInfo.fullName = String.Format("{0}:{1}", senderInfo.appName, senderInfo.senderName);
            // print(senderInfo.fullName));

            //senderInfoListを更新
            senderInfoList.Add(senderInfo);
        }
        //comboBox用のsenderNameListを更新
        senderNameList = senderInfoList.Select(info => info.fullName).ToList();
        _comboBox.list = senderNameList;
    }

    //receiverのソースにsenderを登録
    void SetReceiverSource(string _targetSenderName)
    {
        if (targetSenderName == _targetSenderName) return;
        targetSenderName = _targetSenderName;
        if (spoutReceiver != null)
        {
            spoutReceiver.sourceName = targetSenderName;
        }
        if (syphonReceiver != null)
        {
            var _nameList = targetSenderName.Split(':');
            var _appName = _nameList.Length == 2 ? _nameList[0] : "";
            var _senderName = _nameList[_nameList.Length - 1];
            syphonReceiver.appName = _appName;
            syphonReceiver.serverName = _senderName;
        }

        //comboBoxのselecttを更新
        _comboBox.SelectedItemIndex = Math.Max(senderNameList.IndexOf(targetSenderName), 0);
    }

    #region Control ComboBox

    private bool isDebugMode = false;
    private ComboBox _comboBox;
    private List<SenderInfo> senderInfoList = new List<SenderInfo>();
    private List<string> senderNameList = new List<string>() { "" };

    void CreateComboBox()
    {
        GUIStyle listStyle = new GUIStyle();
        listStyle.normal.textColor = Color.white;
        listStyle.onHover.background =
        listStyle.hover.background = new Texture2D(2, 2);
        listStyle.padding.left =
        listStyle.padding.right =
        listStyle.padding.top =
        listStyle.padding.bottom = 6;

        _comboBox = new ComboBox(new Rect(10, 80, 200, 30), senderNameList, listStyle);
    }

    void OnGUI()
    {
        if (!isDebugMode) return;

        GUISetSize(1024, 768);

        string previous = _comboBox.SelectedItem;
        _comboBox.Show();
        // GUI.Label(new Rect(_comboBox.rect.x, _comboBox.rect.y - 20, 400, 20), "SenderList :");
        autoConnect = GUI.Toggle(new Rect(_comboBox.rect.x, _comboBox.rect.y - 30, 400, 20), autoConnect, " AutoConnect : " + senderName);

        if (_comboBox.SelectedItem != previous)
        {
            autoConnect = false;
            SetReceiverSource(_comboBox.SelectedItem);
        }
    }

    private void GUISetSize(float screenWidth, float screenHeight)
    {
        float horizRatio = Screen.width / screenWidth;
        float vertRatio = Screen.height / screenHeight;
        float ratio = Mathf.Max(horizRatio, vertRatio);
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ratio, ratio, 1));
    }

    #endregion

}

class SenderInfo
{
    public string fullName = "";
    public string appName = "";
    public string senderName = "";

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendFormat("{0}", fullName);
        return builder.ToString();
    }
}