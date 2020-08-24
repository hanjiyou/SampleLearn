//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using XLua;
//using System;
//using System.Reflection;
//using System.Linq;
///// <summary>
///// 记录所有需要用到热修复的代码（和wrapper无关）
///// </summary>
//public static class HotFixList
//{
//    [Hotfix]
//    public static List<Type> hotfixList
//    {
//        get
//        {
////            string[] allowNamespaces = new string[] {
////                "命名空间",
////            };
//
//
//            string[] noAllowNamespaces = new string[]
//            {
//                "DragonBones",
//                "UnityStandardAssets.ImageEffects",
//                "MiniJSON",
//                "XLua",
//                "XLua.CSObjectWrap",
//                "XLua.Cast",
//                "XLua.LuaDLL",
//                "XLua.TemplateEngine",
//                "Spine",
//                "Spine.Unity",
//                "Spine.Unity.Playables",
//                "Spine.Unity.Modules",
//                "Spine.Unity.Modules.AttachmentTools",
//                "FairyGUI",
//                "FairyGUI.Utils",
//                "SharpJson",
//                "Assets.UWA",
//                "GameFramework",
//            };
//
//            string[] noKeyWorkd = new string[]
//            {
//                "CameraFilterPack_",
//                "Yielders",
//                "Fps",
//                "FrameRate",
//                "LuaComponent",
//                "LuaButton",
//                "LuaComboBox",
//                "LuaLabel",
//                "LuaProgressBar",
//                "LuaSlider",
//                "LuaEnforceWindowArgs",
//                "LuaWindow",
//                "Coroutine_Runner",
//                "ResUtility"
//            };
//
//
//            var list = (from type in Assembly.Load("Assembly-CSharp").GetTypes()
//                where (type.IsPublic && !noAllowNamespaces.Contains(type.Namespace) && !CheckContain(type.Name,noKeyWorkd))
//                select type).ToList();
//            //PrintList(list);
//            return list;
//        }
//
//    }
//
//    private static bool CheckContain(string checkStr,string[] strList)
//    {
//        if (strList != null)
//        {
//            for (int i = 0; i < strList.Length; i++)
//            {
//                if (checkStr.Contains(strList[i]))
//                {
//                    return true;
//                }
//            }
//        }
//
//        return false;
//    }
//
//    private static void PrintList(List<System.Type> list)
//    {
//        for (int i = 0; i < list.Count; i++)
//        {
//            Debug.Log(list[i].FullName);
//        }
//    }
//    
//}
