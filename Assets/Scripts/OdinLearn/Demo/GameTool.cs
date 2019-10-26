using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class GameTool : OdinMenuEditorWindow
{
    private GlobalSetting _globalSetting;
    private GMTool _gmTool;
    [MenuItem("MyTools/GameTool %#t")]
    public static void OnOpenWindow()
    {
        var window = GetWindow<GameTool>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
    }
    
    protected override OdinMenuTree BuildMenuTree()
    {
        InitData();
        OdinMenuTree tree=new OdinMenuTree()
        {
            {"系统设置",_globalSetting,EditorIcons.House},
            {"GM工具",_gmTool,EditorIcons.Flag},
            {"出包设置","data",EditorIcons.Info},

        };
        return tree;
    }

    void InitData()
    {
        _globalSetting=new GlobalSetting();
        _globalSetting.InitData();
        _gmTool=new GMTool();
        
    }
    /// <summary>
    /// 全局设置类
    /// </summary>
    [Serializable]
    public class GlobalSetting
    {
#region 序列化成员
        [Title("连接服务器设置")]        
        [ValueDropdown("ServerEnum")]
        public int CurServer;
        
        [DisableInEditorMode]
        public GameObject GameToolData;

        [FoldoutGroup("服务器信息",Expanded = true)] 
        [DisplayAsString]
        public string NameIndex;
        [FoldoutGroup("服务器信息",Expanded = true)] 
        [DisplayAsString]
        public string ServerName;

        [Title("调试设置")] 
        public bool LuaDebug;
        [InfoBox("是否在屏幕上显示Fps,内存占用信息",InfoMessageType.Info)]
        public bool ShowDebugInfo;
        #endregion
private IEnumerable ServerEnum = new ValueDropdownList<int>()
        {
            {"测试服1", 0},
            {"测试服2", 1},
            {"外服", 2}
        };

        public void InitData()
        {
            NameIndex = "2";
            ServerName = "BILIBILI服";
        }
    }
    /// <summary>
    /// GM工具类
    /// </summary>
    public class GMTool
    {
        [Title("GMTools")]
        [DisplayAsString]
        public string Tip = "#绿色为可调参数，#其余直接使用";
        [FoldoutGroup("变速")] 
        public int TimeScale = 1;
        [FoldoutGroup("变速")]
        [Button(("还原"),ButtonSizes.Medium),GUIColor(0,1,1)]
        void OnChangeSpeedBtn()
        {
            Loger.Log($"hhh timeScale={TimeScale}");
            TimeScale = 1;
        }

        [FoldoutGroup("结算")] 
        [EnumToggleButtons]
        public ResultMenu Result;
        [FoldoutGroup("结算")]
        [HorizontalGroup("结算/horGroup",MarginLeft = 0.25f,MarginRight = 0.25f)]
        [Button("结算",ButtonSizes.Medium),GUIColor(0,1,0)]
        void OnComputeResult()
        {
            switch (Result)
            {
                case ResultMenu.胜:
                    Loger.Log("you win");
                    break;
                case ResultMenu.负:
                    Loger.Log("you lost");
                    break;
            }
        }

        [FoldoutGroup("剧情测试")] public int LevelStoryId = 0;

        [FoldoutGroup("剧情测试")]
        [HorizontalGroup("剧情测试/horGroup",MarginLeft = 0.1f,MarginRight = 0.1f)]
        [Button("开启",ButtonSizes.Medium),GUIColor(0,1,0)]
        void OnStartBtn()
        {
            Loger.Log("start btn");
        }
        [FoldoutGroup("剧情测试")]
        [HorizontalGroup("剧情测试/horGroup",MarginRight = 0.1f)]
        [Button("关闭",ButtonSizes.Medium),GUIColor(0,1,0)]
        void OnCloseStoryBtn()
        {
            Loger.Log("OnCloseStoryBtn");
        }
        
         public  enum ResultMenu
        {
            胜,
            负
        }
    }
}
