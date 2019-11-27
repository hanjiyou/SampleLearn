using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    
    [MenuItem("MyTools/BundleBuild/ClearAllABNames",false,13)]
    public static void ClearAllABNames()
    {
        string[] allABNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < allABNames.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(allABNames[i], true);
        }
    }

    #endregion


    [MenuItem("MyTools/BundleBuild/BuildWin",false,23)]
    public static void BuildWin()
    {
        NameAllResource();
        BuildBundle(BuildTarget.StandaloneWindows);
    }

    [MenuItem("MyTools/BundleBuild/GetManifestMd5",false,12)]
    public static void GetManifestMd5()
    {
        string mainfestPath = "D:/Windows/MyAb/MyAb";
        LogTool.Log("md5="+ResourceUtility.GetMD5(mainfestPath));
    }

    #region 内部功能方法

    /// <summary>
    /// 为资源命名AB名
    /// </summary>
    private static void NameAllResource()
    {
        ClearAllABNames();
        string resPath = PathUtility.CombinePath(PathUtility.AssetDataPath, PathUtility.ResourcePathName);
        string[] dirs= Directory.GetDirectories(resPath);
        var files = Directory.GetFiles(resPath, ".", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta"))
                continue;
            string replaceFilePath = files[i].Replace('\\', '/');
            //相对于resources的带文件名路径
            string fileResPathWithFileName = replaceFilePath.Substring(resPath.Length+1, replaceFilePath.Length - resPath.Length-1);
            string fileResPath=string.Empty;
//            if(fileResPathWithFileName.Contains(""))
            fileResPath = fileResPathWithFileName.ToLower()
                .Substring(0, fileResPathWithFileName.LastIndexOf("/"));
            AssetImporter assetImporter=AssetImporter.GetAtPath("Assets/Resources/"+fileResPathWithFileName);
            Debug.Log($"{i}={fileResPath}");
            assetImporter.assetBundleName = fileResPath;
        }
    }
    
    private static void BuildBundle(BuildTarget buildTarget)
    {
        GetCommandLineArgs();
        
        string rootPath = PathUtility.ProjectBuildABPath;
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        
        BuildPipeline.BuildAssetBundles(rootPath, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
        LogTool.Log("BuildBundle Complete");
    }

    [MenuItem("MyTools/BundleBuild/CreateVersionFile")]
    public static void CreateVersionFile()
    {
        string manifestPathWithName =PathUtility.ProjectBuildABPath;
//        var loadedAB = AssetBundle.GetAllLoadedAssetBundles().ToArray();
//        for (int i = 0; i < loadedAB.Length; i++)
//        {
//            LogTool.Log($"{i}={loadedAB[i]}");
//        }

//        return;
        AssetBundle.UnloadAllAssetBundles(true);
        
        AssetBundle manifestAssetBundle= AssetBundle.LoadFromFile(manifestPathWithName);
        AssetBundleManifest assetBundleManifest = manifestAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string versionFilePath=PathUtility.CombinePath(PathUtility.ProjectBuildABPath, PathUtility.VersionFileName);
        StreamWriter streamWriter=new StreamWriter(new FileStream(versionFilePath, FileMode.OpenOrCreate));
        streamWriter.WriteLine(0);
        streamWriter.Write(PathUtility.AssetBundlePathName+"|");
        string md5 = ResourceUtility.GetMD5(manifestPathWithName);
        streamWriter.Write(md5+"|");
        FileInfo manifestFileInfo=new FileInfo(manifestPathWithName);
        streamWriter.WriteLine(manifestFileInfo.Length);
        string[] allABs= assetBundleManifest.GetAllAssetBundles();
        for (int i = 0; i < allABs.Length; i++)
        {
            LogTool.Log($"{i}={allABs[i]}");
            streamWriter.Write(allABs[i]+"|");
            string itemPath = PathUtility.CombinePath(PathUtility.ProjectBuildABPath, PathUtility.AssetBundlePathName);
            string fileItemMd5 = ResourceUtility.GetMD5(itemPath);
            streamWriter.Write(fileItemMd5+"|");
            FileInfo fileItemFileInfo=new FileInfo(itemPath);
            streamWriter.WriteLine(fileItemFileInfo.Length);
        }
        
        streamWriter.Close();
    }
    
    private static void GetCommandLineArgs()
    {
        string[] commandLineArgs= Environment.GetCommandLineArgs();
        for (int i = 0; i < commandLineArgs.Length; i++)
        {
            LogTool.Log($"{i}={commandLineArgs[i]}");
        }
    }

    #endregion
   
}
