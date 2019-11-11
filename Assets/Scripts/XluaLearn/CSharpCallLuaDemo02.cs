using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class CSharpCallLuaDemo02 : MonoBehaviour
{
    private LuaEnv _luaEnv;
    private string _luaScript = @"
    a=1
    b='hello world'
    c=true
    d=1.1
    ";
    private void Awake()
    {
        _luaEnv=new LuaEnv();
    }

    // Start is called before the first frame update
    void Start()
    {
        _luaEnv.DoString(_luaScript);
        Loger.Log($"luaA={_luaEnv.Global.Get<int>("a")}");
        Loger.Log($"luaB={_luaEnv.Global.Get<string>("b")}");
        Loger.Log($"luaC={_luaEnv.Global.Get<bool>("c")}");
        Loger.Log($"luaD={_luaEnv.Global.Get<float>("d")}");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _luaEnv.Dispose();
    }
}
