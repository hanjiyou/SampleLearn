using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

public struct Param1
{
    public int x;
    public string y;
}

public class XLuaManager
{
    private LuaEnv _luaEnv;

    public LuaEnv LuaEnv
    {
        get => _luaEnv;
        private set => _luaEnv = value;
    }

    private static XLuaManager Instance;
    public static XLuaManager GetInstance()
    {
        if (Instance == null)
        {
            Instance=new XLuaManager();
        }

        return Instance;
    }

    private XLuaManager()
    {
        _luaEnv=new LuaEnv();
        LogTool.Log("hhh XLuaCSharp Constructor");
//        _luaEnv.AddLoader(SetSelfLoader);
        _luaEnv.AddLoader(SetSelfLoader);
    }

    #region 对外功能方法

    public object[] DoString(string luaContent)
    {
        return  _luaEnv.DoString(luaContent);
    }
    public object[] DoLuaFile(string file)
    {
        return _luaEnv.DoString(string.Format("return require '{0}'", file));
    }

    public LuaTable GetGlobalTable()
    {
        return _luaEnv.Global;
    }
    public LuaTable NewLuaTable()
    {
        return _luaEnv.NewTable();
    }

    /// <summary>
    /// 加载lua方法
    /// </summary>
    /// <returns></returns>
    public LuaFunction GetLuaFunction(string functionName)
    {
        return _luaEnv.Global.Get<LuaFunction>(functionName);
    }

    public void DisposeEnv()
    {
        _luaEnv?.Dispose();
    }
    #endregion

    #region 内部方法

    /// <summary>
    /// 设置自定义的加载器（xlua也有内置的loader，但是只限于Resources文件夹下）
    /// filePath是文件相对路径 "LoadAssetAtPath"是相对路径 指定的目录需要从Assets开始
    /// </summary>
    private byte[] SetSelfLoader(ref string filePath)
    {
        //方法1 通过TextAsset加载lua文件
//        string path = "Assets/Res/LuaFile/" + filePath+".bytes";
//        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
//        return textAsset.bytes;
        //方法2 通过io流
        string path = Application.dataPath + "/Res/LuaFile/" + filePath + ".bytes";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
    }


    #endregion
//
//    public void TestFunc(string luaContent)
//    {
//        LogTool.Log("hhh111 XLuaCSharp TestFunc luaContent=",luaContent);
//    }
//    public void TestFunc(int luaContent)
//    {
//        LogTool.Log("hhh222 XLuaCSharp TestFunc luaContent=",luaContent);
//    }
///// <summary>
///// 实参:lua调用C#函数的多个实参顺序是:从左到右普通函数+ref。out不填实参，即使out不在最后位置，out也不占实参
///// 返回值:lua多个返回值顺序是：C#函数返回值，函数参数从左到右的ref和out
///// 返回值和特殊参数(ref和out)在lua端必须用参数接受
///// </summary>
///// <param name="p1"></param>
///// <param name="p2"></param>
///// <param name="p3"></param>
//    public double ComplexFunc(Param1 p1,out string p2, ref int p3)
//    {
//        LogTool.Log("hhh222 XLuaCSharp ComplexFunc");
//        LogTool.Log($"p1={p1.x},{p1.y},p3={p3}");
//        p1.x = 111;
//        p1.y = "p1yyyyyy";
//        p2 ="this is p3";
//        p3 = 222;
//
//        return Mathf.PI;
//    }
}
