using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 事件发布者(事件在外部被订阅者订阅，但是执行只能在发布者内部执行)
/// </summary>
public class GameEventPublisher
{
    /// <summary>
    /// 声明事件
    /// </summary>
    public event EventHandler<GameEventArgs> RaiseCustomEvent;

    public void DoSomething()
    {
        OnRaiseCustomEvent(new GameEventArgs("Do Something"));
    }

    protected virtual void OnRaiseCustomEvent(GameEventArgs e)
    {
        //进行临时拷贝，为了避免最后一个订阅者推定的临界条件，在事件触发之前马上检查是否为空
        EventHandler<GameEventArgs> handler = RaiseCustomEvent;
        if (handler != null)
        {
            e.Msg += DateTime.Now;
            //使用()运算符触发事件
            handler(this, e);
        }
    }
}
