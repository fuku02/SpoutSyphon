using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ComboBox class.
/// Based on ComboBox (extension of Popup _list) by Hyungseok Seo and Eric HainesPopup _list created by Eric Haines
/// http://wiki.unity3d.com/index.php?title=PopupList
/// </summary>
/// 
public class ComboBox
{
    private static bool _forceToUnShow = false;
    private static int _useControlID = -1;
    private bool _isClickedComboButton = false;
    private int _selectedItemIndex = 0;

    private Rect _rect;
    public Rect rect
    {
        get { return _rect; }
    }
    private GUIContent _buttonContent;
    private List<string> _list;
    public List<string> list
    {
        get { return _list; }
        set
        {
            _list = value;
            //create _listContent from _list
            _listContent = new GUIContent[list.Count];
            for (int i = 0; i < _list.Count; i++)
            {
                _listContent[i] = new GUIContent(list[i].ToString());
            }
            //set initial selection
            this._buttonContent = _listContent[_selectedItemIndex];
        }
    }
    private GUIContent[] _listContent;
    private string _buttonStyle;
    private string _boxStyle;
    private GUIStyle _listStyle;

    public ComboBox(Rect rect, List<string> list)
    {
        // Make a GUIStyle that has a solid white hover/onHover background to indicate highlighted items
        _listStyle = new GUIStyle();
        _listStyle.normal.textColor = Color.white;
        _listStyle.onHover.background =
        _listStyle.hover.background = new Texture2D(2, 2);
        _listStyle.padding.left =
        _listStyle.padding.right =
        _listStyle.padding.top =
        _listStyle.padding.bottom = 4;

        this._rect = rect;
        this._list = list;
        this._buttonStyle = "button";
        this._boxStyle = "box";

        //create _listContent from _list
        _listContent = new GUIContent[list.Count];
        for (int i = 0; i < _list.Count; i++)
        {
            _listContent[i] = new GUIContent(list[i].ToString());
        }
        //set initial selection
        this._buttonContent = _listContent[_selectedItemIndex];
    }

    public ComboBox(Rect rect, List<string> list, GUIStyle listStyle)
    {
        this._rect = rect;
        this._list = list;
        this._buttonStyle = "button";
        this._boxStyle = "box";
        this._listStyle = listStyle;

        //create _listContent from _list
        _listContent = new GUIContent[list.Count];
        for (int i = 0; i < _list.Count; i++)
        {
            _listContent[i] = new GUIContent(list[i].ToString());
        }
        //set initial selection
        this._buttonContent = _listContent[_selectedItemIndex];
    }

    public ComboBox(Rect rect, List<string> list, string buttonStyle, string boxStyle, GUIStyle listStyle)
    {
        this._rect = rect;
        this._list = list;
        this._buttonStyle = buttonStyle;
        this._boxStyle = boxStyle;
        this._listStyle = listStyle;

        //create _listContent from _list
        _listContent = new GUIContent[list.Count];
        for (int i = 0; i < _list.Count; i++)
        {
            _listContent[i] = new GUIContent(list[i]);
        }
        //set initial selection
        this._buttonContent = _listContent[_selectedItemIndex];
    }

    public int Show()
    {
        if (_forceToUnShow)
        {
            _forceToUnShow = false;
            _isClickedComboButton = false;
        }

        bool done = false;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.MouseUp:
                {
                    if (_isClickedComboButton)
                    {
                        done = true;
                    }
                }
                break;
        }

        if (GUI.Button(_rect, _buttonContent, _buttonStyle))
        {
            if (_useControlID == -1)
            {
                _useControlID = controlID;
                _isClickedComboButton = false;
            }

            if (_useControlID != controlID)
            {
                _forceToUnShow = true;
                _useControlID = controlID;
            }
            _isClickedComboButton = true;
        }

        if (_isClickedComboButton)
        {
            Rect _listRect = new Rect(_rect.x, _rect.y + _listStyle.CalcHeight(_listContent[0], 1.0f),
                _rect.width, _listStyle.CalcHeight(_listContent[0], 1.0f) * _listContent.Length);

            GUI.Box(_listRect, "", _boxStyle);
            int new_selectedItemIndex = GUI.SelectionGrid(_listRect, _selectedItemIndex, _listContent, 1, _listStyle);
            if (new_selectedItemIndex != _selectedItemIndex)
            {
                _selectedItemIndex = new_selectedItemIndex;
                // _buttonContent = _listContent[_selectedItemIndex];
            }
        }

        if (done)
        {
            _isClickedComboButton = false;
        }

        return _selectedItemIndex;
    }

    public string SelectedItem
    {
        get { return _list[_selectedItemIndex]; }
    }

    public int SelectedItemIndex
    {
        get { return _selectedItemIndex; }
        set
        {
            _selectedItemIndex = value;
            _buttonContent = _listContent[_selectedItemIndex];
        }
    }
}