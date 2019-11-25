using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生命自定义事件参数
/// </summary>
public class GameEventArgs:EventArgs
{
    private string _msg;
    public string Msg
    {
        get { return _msg;}
        set { _msg = value; }
    }
    public GameEventArgs(string temMsg)
    {
        _msg = temMsg;
    }
}
