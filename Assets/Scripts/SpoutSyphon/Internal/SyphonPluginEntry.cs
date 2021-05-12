using System;
using System.Runtime.InteropServices;
using UnityEngine;

static class SyphonPluginEntry
{
    #region Native plugin entry points for Syphon

    [DllImport("KlakSyphon")]
    internal static extern IntPtr Plugin_CreateServerList();

    [DllImport("KlakSyphon")]
    internal static extern void Plugin_DestroyServerList(IntPtr list);

    [DllImport("KlakSyphon")]
    internal static extern int Plugin_GetServerListCount(IntPtr list);

    [DllImport("KlakSyphon")]
    internal static extern IntPtr Plugin_GetNameFromServerList(IntPtr list, int index);

    [DllImport("KlakSyphon")]
    internal static extern IntPtr Plugin_GetAppNameFromServerList(IntPtr list, int index);

    #endregion
}