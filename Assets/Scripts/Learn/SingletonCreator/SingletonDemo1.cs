using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDemo1 : ISingleton
{
    private  SingletonDemo1()
    {
        LogTool.Log("单例demo 构造");
    }
    public void OnSingletonInit()
    {
        LogTool.Log("单例demo singletonDemo1初始化");
    }
}
