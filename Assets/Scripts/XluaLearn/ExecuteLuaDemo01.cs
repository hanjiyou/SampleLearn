using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// 执行lua的3种方式
///①DoString 不建议
/// </summary>
public class ExecuteLuaDemo01 : MonoBehaviour
{
    private LuaEnv _luaEnv;
    private void Start()
    {
//执行lua的3种方式
    //方法1 DoString 不建议
        Debug.Log("1111Helloworld");
        LogTool.Log("11111Helloworld");
        _luaEnv=new LuaEnv();
        //直接执行lua代码(控制台会有前缀"LUA:")
        _luaEnv.DoString("print('hello World')");
        //lua调用C#
        _luaEnv.DoString("CS.UnityEngine.Debug.Log('2222helloworld')");
        _luaEnv.DoString("CS.Loger.Log('2222helloworld')");
    //方法2 加载lua文件
        _luaEnv.DoString("require 'XLua/executeLuaDemo01'");
        //_luaEnv.DoString("require 'byfile'");
    //方法3 自定义loader
        _luaEnv.AddLoader((ref string filepath) =>
        {
            if (filepath == "myLoader")
            {
                string scirpt = "print('i am myLoader')" +
                                "return {intVar=99}";
                return System.Text.Encoding.UTF8.GetBytes(scirpt);
            }
            LogTool.Log($"filePath={filepath}");
            return null;
        });
        _luaEnv.DoString("print('添加自定义loader的require',require ('myLoader').intVar)");
    }

    private void OnDestroy()
    {
        _luaEnv.Dispose();
    }
}
