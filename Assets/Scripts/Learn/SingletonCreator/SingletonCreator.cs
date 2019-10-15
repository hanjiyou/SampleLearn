using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class SingletonCreator
{
    public static T CreateSingleton<T>() where T : class,ISingleton
    {
        var constructors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);//获取私有的、包含实例成员的构造函数
        var constructor = Array.Find(constructors, con => con.GetParameters().Length == 0);//只使用无参构造
        if (constructor == null)
        {
            throw new Exception("Non-Public Constructor() not found! in "+typeof(T));
        }

        var newInstance = constructor.Invoke(null) as T;
        if (newInstance != null)
        {
            newInstance.OnSingletonInit();
            return newInstance;
        }
        else
        {
            throw new Exception(typeof(T)+"单例对象创建失败");
        }
    }
}
