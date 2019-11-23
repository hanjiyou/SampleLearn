using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public LanguageType LanguageType;
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