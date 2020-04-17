using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
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
//        _luaEnv.DoString("require 'XLua/LuaCallCSharp'");
        _luaEnv.DoString("require 'XLua/testSuffix'");
//        _luaEnv.DoString(luaCodeContext);
//        transform.SetParent(GameObject.Find("myNewObj").transform);
//        TextAsset textAsset = Resources.Load<TextAsset>("Xlua/testSuffix.lua");
//        if (textAsset != null)
//        {
//            Debug.Log("hhh bytes="+textAsset.bytes.Length+"text="+textAsset.text);
//        }
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
