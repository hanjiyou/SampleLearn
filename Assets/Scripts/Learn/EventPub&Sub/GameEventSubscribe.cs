using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSubscribe
{
    private string id;

    public GameEventSubscribe(string ID, GameEventPublisher pub)
    {
        id = ID;
        //使用C# 2.0语法 订阅事件
        pub.RaiseCustomEvent += HandleCustomEvent;
    }

    void HandleCustomEvent(object sender,GameEventArgs args)
    {
        LogTool.Log($"hhh hello this is id={id} Sub! receive msg content="+args.Msg);
        
    }
    
}
