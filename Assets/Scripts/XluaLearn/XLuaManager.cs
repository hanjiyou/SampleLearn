using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Param1
{
    public int x;
    public string y;
}

public class XLuaManager
{
    public static XLuaManager Instance;
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
        LogTool.Log("hhh XLuaCSharp Constructor");
    }

    public void TestFunc(string luaContent)
    {
        LogTool.Log("hhh111 XLuaCSharp TestFunc luaContent=",luaContent);
    }
    public void TestFunc(int luaContent)
    {
        LogTool.Log("hhh222 XLuaCSharp TestFunc luaContent=",luaContent);
    }
/// <summary>
/// 实参:lua调用C#函数的多个实参顺序是:从左到右普通函数+ref。out不填实参，即使out不在最后位置，out也不占实参
/// 返回值:lua多个返回值顺序是：C#函数返回值，函数参数从左到右的ref和out
/// 返回值和特殊参数(ref和out)在lua端必须用参数接受
/// </summary>
/// <param name="p1"></param>
/// <param name="p2"></param>
/// <param name="p3"></param>
    public double ComplexFunc(Param1 p1,out string p2, ref int p3)
    {
        LogTool.Log("hhh222 XLuaCSharp ComplexFunc");
        LogTool.Log($"p1={p1.x},{p1.y},p3={p3}");
        p1.x = 111;
        p1.y = "p1yyyyyy";
        p2 ="this is p3";
        p3 = 222;

        return Mathf.PI;
    }
}
