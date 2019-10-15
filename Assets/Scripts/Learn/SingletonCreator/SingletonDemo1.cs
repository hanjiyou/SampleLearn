using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDemo1 : ISingleton
{
    private  SingletonDemo1()
    {
        Loger.Log("单例demo 构造");
    }
    public void OnSingletonInit()
    {
        Loger.Log("单例demo singletonDemo1初始化");
    }
}
