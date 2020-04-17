using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Demo1_DIYLoader : MonoBehaviour
{
    private Dictionary<string,int> toLuaTableDic=new Dictionary<string, int>();
    private LuaFunction _luaFunction;
    private void Awake()
    {
        toLuaTableDic["one"] = 1;
        toLuaTableDic["two"] = 2;
        toLuaTableDic["three"] = 3;
        InitGlobalData();
    }

    // Start is called before the first frame update
    void Start()
    {
//        //1.使用默认loader 加载Resource下的lua
//        XLuaManager.GetInstance().DoLuaFile("XLua/testSuffix");
//        XLuaManager.GetInstance().DoLuaFile("helloWordDefaultLoader");
//        LogTool.Log("hhh 再次require不会重复执行");

        //2.使用自定义loader
        XLuaManager.GetInstance().DoLuaFile("LuaEntrance");
        _luaFunction = XLuaManager.GetInstance().GetGlobalTable().Get<LuaFunction>("GlobalFunction1");
        long result= (long)_luaFunction.Call(1,2,"haha")[0];
    }

    void NewTable()
    {
        LuaTable constantTable = XLuaManager.GetInstance().NewLuaTable();
        foreach (var VARIABLE in toLuaTableDic)
        {
            constantTable.Set(VARIABLE.Key,VARIABLE.Value);
        }
        XLuaManager.GetInstance().GetGlobalTable().Set("ConstantTable",constantTable);
    }

    void InitGlobalData()
    {
        NewTable();
        XLuaManager.GetInstance().GetGlobalTable().Set("IsDebugLua",true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        XLuaManager.GetInstance().DisposeEnv();
    }
}
