using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BundleEditor : Editor
{
    #region 基础测试方法
    
    /// <summary>
    /// ①name中指定快捷键 必须用空格隔开，如果不带特殊字符 则用'_'开头
    /// ②两个用分隔线分隔的条目。当priority参数之间的分隔符大于10 时
    /// </summary>
    [MenuItem("MyTools/BundleBuild/GetAssetBundleNames #LEFT",false,10)]
    public static void GetAssetBundleNames()
    {
        //获取所有的自定义的abb
        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < assetBundleNames.Length; i++)
        {
            LogTool.Log($"{i}={assetBundleNames[i]}");
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("MyTools/BundleBuild/RemoveUnuseAssetBundleNames",false,11)]
    public static void RemoveUnuseAssetBundleNames()
    {
        LogTool.Log("移除前");
        GetAssetBundleNames();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        LogTool.Log("移除后");
        GetAssetBundleNames();
    }

    #endregion


    [MenuItem("MyTools/BundleBuild/BuildWin",false,23)]
    public static void BuildWin()
    {
        BuildBundle(BuildTarget.StandaloneWindows);
    }

    [MenuItem("MyTools/BundleBuild/GetManifestMd5",false,12)]
    public static void GetManifestMd5()
    {
        string mainfestPath = "D:/Windows/MyAb/MyAb";
        LogTool.Log("md5="+FileUtility.GetMD5(mainfestPath));
    }
    private static void BuildBundle(BuildTarget buildTarget)
    {
        GetCommandLineArgs();
        
        string rootPath = "D:/Windows/MyAb";
        string manifest = rootPath + "/MyAb";
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        
        BuildPipeline.BuildAssetBundles(rootPath, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
        LogTool.Log("BuildBundle Complete");
    }
    
    private static void GetCommandLineArgs()
    {
        string[] commandLineArgs= Environment.GetCommandLineArgs();
        for (int i = 0; i < commandLineArgs.Length; i++)
        {
            LogTool.Log($"{i}={commandLineArgs[i]}");
        }
    }
}
