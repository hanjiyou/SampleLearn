using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaCallCSharpDemo03 : MonoBehaviour
{
    private LuaEnv _luaEnv;
    private string luaCodeContext = @"

";
    
    private void Awake()
    {
        _luaEnv=new LuaEnv();
    }

    // Start is called before the first frame update
    void Start()
    {
        _luaEnv.DoString("require 'XLua/LuaCallCSharp'");
//        _luaEnv.DoString(luaCodeContext);
//        transform.SetParent(GameObject.Find("myNewObj").transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
