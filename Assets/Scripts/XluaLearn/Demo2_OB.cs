using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp()]
public class Equip
{
    public int EquipId = 111;
    public string EquipName = "defaultName";
    public int EquipColor = 1;
    public int EquipDamage = 10;

    public void LogInfo()
    {
        LogTool.Log($"logInfo id={EquipId} name={EquipName} color={EquipColor} damage={EquipDamage}");
    }
}
public class Demo2_OB : MonoBehaviour
{
    public List<Equip> equipsInjectionData=new List<Equip>();
    public TextAsset EquipLuaEntrance;
    private LuaTable scriptEnv;//为当前脚本设置一个独立的环境

    private Action awakeAct;
    private Action logEquipInfo;
    // Start is called before the first frame update
    void Start()
    {
        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        scriptEnv=XLuaManager.GetInstance().NewLuaTable();
        LuaTable meta = XLuaManager.GetInstance().NewLuaTable();
        meta.Set("__index",XLuaManager.GetInstance().GetGlobalTable());//设置元表
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        
        scriptEnv.Set("self", this);
        equipsInjectionData.Add(new Equip(){EquipId = 112,EquipName = "青龙偃月刀",EquipColor = 3,EquipDamage = 100});
        equipsInjectionData.Add(new Equip(){EquipId = 113,EquipName = "青龙偃月刀33",EquipColor = 33,EquipDamage = 103});
        //为当前表注入数据
        scriptEnv.Set("equipDatas",equipsInjectionData);

        XLuaManager.GetInstance().LuaEnv.DoString(EquipLuaEntrance.bytes,"Demo2_OB",scriptEnv);

        awakeAct = scriptEnv.Get<Action>("awake");
        awakeAct?.Invoke();
        Action logEquipInfo = scriptEnv.Get<Action>("LogEquipInfo");
        logEquipInfo?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        scriptEnv?.Dispose();
    }

    private void OnApplicationQuit()
    {
//        XLuaManager.GetInstance().DisposeEnv();
    }
}
