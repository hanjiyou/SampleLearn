using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyClass
{
    public int Age;
    public string Name;
}
/// <summary>
/// 系统配置数据
/// </summary>
public class SystemConfig : ScriptableObject
{
    public string GameVersion;
    public bool LoadFromBundle;
    public string LoginIp;
    public int Port;
    public bool UserSdk;
    public SdkType SdkType;
    public bool Debug;
    public bool Warning;
    public bool Error;
    private LanguageType LanguageType;

    private List<MyClass> TestSerialList;

    public void InitList()
    {
        if (TestSerialList == null || TestSerialList.Count == 0)
        {
            LogTool.Log("hhh 为空 需复制");
            TestSerialList = new List<MyClass>();
            TestSerialList.Add(new MyClass(){Name = "haha",Age = 5});
            TestSerialList.Add(new MyClass(){Name = "aa",Age = 6});
        }
        else
        {
            LogTool.Log("hhh 不为空 不需复制"+TestSerialList.Count);

        }
    }
}
/// <summary>
/// 游戏版本平台类型
/// </summary>
public enum SdkType
{
    None,
    Tencent,
    XiaoMi,
    HuaWei,
    IOS,
}

public enum LanguageType
{
    China_CN,
    China_TW,
    Korea
}