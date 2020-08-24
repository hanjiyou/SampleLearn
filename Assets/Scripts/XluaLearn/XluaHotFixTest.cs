using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class XluaHotFixTest : MonoBehaviour
{
    private void Awake()
    {
        TestHotFix();
        XLuaManager.GetInstance().DoLuaFile("BugFix/BugFix");

    }

    // Start is called before the first frame update
    void Start()
    {
        TestHotFix();
    }

    void TestHotFix()
    {
        LogTool.Log("hhh 替换前");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        XLuaManager.GetInstance().DisposeEnv();
    }
}
