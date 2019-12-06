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
    
    [MenuItem("MyTools/BundleBuild/ClearAllABNames",false,12)]
    public static void ClearAllABNames()
    {
        string[] allABNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < allABNames.Length; i++)  
        {
            AssetDatabase.RemoveAssetBundleName(allABNames[i], true);
        }
    }

    [MenuItem("MyTools/BundleBuild/ClearNullDir",false,13)]
    public static void ClearNullDir()
    {
        string path = @"D:\Windows\SampleLearn\a";
        RemoveNullDir(new DirectoryInfo(path));
    }
    #endregion

    #region 主方法

    /// <summary>
    /// 创建windows ab包。先为所有资源ab命名，再build ab包
    /// ab输出路径为:Assets父路径/Build/platformFolder
    /// </summary>
    [MenuItem("MyTools/BundleBuild/BuildWin",false,30)]
    public static void BuildWin()
    {
        NameAllResource();
        BuildBundle(BuildTarget.StandaloneWindows);
    }

    #endregion

    #region 内部功能方法

    /// <summary>
    /// 为资源命名AB名
    /// </summary>
    private static void NameAllResource()
    {
        ClearAllABNames();
        string resPath = PathUtility.CombinePath(PathUtility.AssetDataPath, PathUtility.ResourcePathName);
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
                .Substring(0, fileResPathWithFileName.LastIndexOf("."));
            //修改资源的ab名，需要通过AssetImporter,而AssetImporter必须指定从Assets开始的路径（从盘符开始的绝对路径都不行）
            AssetImporter assetImporter=AssetImporter.GetAtPath("Assets/Resources/"+fileResPathWithFileName);
            assetImporter.assetBundleName = fileResPath+PathUtility.ABSuffix;
        }
    }
    
    private static void BuildBundle(BuildTarget buildTarget)
    {
        GetCommandLineArgs();
        
        string bundleFolderRootPath =PathUtility.CombinePath(PathUtility.ProjectBuildABPath,PathUtility.GetTargetPlatformPath(buildTarget));
        string manifestFilePath = PathUtility.CombinePath(bundleFolderRootPath, PathUtility.GetTargetPlatformPath(buildTarget));
        if (!Directory.Exists(bundleFolderRootPath))
        {
            Directory.CreateDirectory(bundleFolderRootPath);
        }

        string oldMd5 = string.Empty;
        if (File.Exists(manifestFilePath))
        {
            oldMd5 = ResourceUtility.GetMD5(manifestFilePath);
        }
        BuildPipeline.BuildAssetBundles(bundleFolderRootPath, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
        string newMd5=ResourceUtility.GetMD5(manifestFilePath);
        if (oldMd5 != newMd5)
        {
            RemoveObsoleteBundle(bundleFolderRootPath);
            RemoveNullDir(new DirectoryInfo(bundleFolderRootPath));
            CreateVersionFile(bundleFolderRootPath);
        }
        LogTool.Log("BuildBundle Complete");
    }

    /// <summary>
    /// 移除废弃的资源(删除文件夹里残存的，但是新清单文件里不存在的文件)
    /// </summary>
    /// <param name="buildABRootPath"></param>
    private static void RemoveObsoleteBundle(string buildABRootPath)
    {
        // 统计所有的ab文件
        HashSet<string> fileList = new HashSet<string>();
        var rootDir = new DirectoryInfo(buildABRootPath);
        var childDirs = rootDir.GetDirectories();
        foreach (var child in childDirs)
        {
            GetAllFileInDir(child, fileList, ".manifest");
        }

        // 统计有效的ab文件
        var manifestPath = PathUtility.CombinePath(buildABRootPath, PathUtility.AssetBundlePathName);
        var ab = AssetBundle.LoadFromFile(manifestPath);
        AssetBundleManifest manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        var allAssets = manifest.GetAllAssetBundles();
        ab.Unload(true);
        HashSet<string> bundleList = new HashSet<string>();
        foreach (var assetName in allAssets)
        {
            var fileInfo = new FileInfo(PathUtility.CombinePath(buildABRootPath, assetName));
            if (fileInfo != null)
            {
                bundleList.Add(fileInfo.FullName);
            }
        }
        
        // 找到废弃的ab文件
        fileList.ExceptWith(bundleList);

        // 删除废弃的ab文件
        foreach (var obsoleteFile in fileList)
        {
            LogTool.Log("删除残留文件"+obsoleteFile);
            File.Delete(obsoleteFile);
            File.Delete(string.Format("{0}.manifest", obsoleteFile));
        }
    }

    static void RemoveNullDir(DirectoryInfo directoryInfo)
    {
        var dirList = directoryInfo.GetDirectories();
        for (int i = 0; i < dirList.Length; i++)
        {
            DirectoryInfo dirItemInfo=new DirectoryInfo(dirList[i].FullName);
            if (dirItemInfo.GetFiles().Length == 0 && dirItemInfo.GetDirectories().Length==0)
            {
                LogTool.Log("删除文件夹:"+dirItemInfo.FullName);
                Directory.Delete(dirItemInfo.FullName);
            }
            else
            {
                RemoveNullDir(dirItemInfo);
            }
        }
    }
    
    /// <summary>
    /// 获取文件夹下的所有文件，可以递归子目录
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="fileList"></param>
    /// <param name="delExtension"></param>
    private static void GetAllFileInDir(DirectoryInfo dir, HashSet<string> fileList, string delExtension = null)
    {
        var files = dir.GetFiles();
        foreach (var file in files)
        {
            if (file.Extension == delExtension) continue;
            fileList.Add(file.FullName);
        }

        var childDirs = dir.GetDirectories();
        foreach (var child in childDirs)
        {
            GetAllFileInDir(child, fileList);
        }
    }

    
//    [MenuItem("MyTools/BundleBuild/CreateVersionFile")]
    public static void CreateVersionFile(string buildABRootPath)
    {
        string manifestPathWithName =PathUtility.CombinePath(buildABRootPath,PathUtility.AssetBundlePathName);
        var loadedAB = AssetBundle.GetAllLoadedAssetBundles().ToArray();
        if (loadedAB.Length > 0)
        {
            for (int i = 0; i < loadedAB.Length; i++)
            {
                LogTool.Log($"卸载已加载资源{i}={loadedAB[i]}");
            }
            AssetBundle.UnloadAllAssetBundles(true);
            return;
        }
        
        AssetBundle manifestAssetBundle= AssetBundle.LoadFromFile(manifestPathWithName);
        AssetBundleManifest assetBundleManifest = manifestAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] allABPath= assetBundleManifest.GetAllAssetBundles();//通过清单文件，获取所有的ab包路径（相对于build的路径）
        manifestAssetBundle.Unload(true);//如果不卸载 会报错：IO异常，sharing violation非法共享

        Dictionary<string, string> fileHashList = new Dictionary<string, string>();
        string versionFilePath=PathUtility.CombinePath(PathUtility.ProjectBuildABPath, PathUtility.VersionFileName);
        string md5 = ResourceUtility.GetMD5(manifestPathWithName);
        fileHashList[PathUtility.AssetBundlePathName] = ResourceUtility.GetMD5(manifestPathWithName);
        foreach (var asset in allABPath)
        {
            fileHashList[asset] = ResourceUtility.GetMD5(PathUtility.CombinePath(PathUtility.ProjectBuildABPath,asset));
        }
        
        int version = 0;
        using (FileStream fileStream=new FileStream(versionFilePath, FileMode.OpenOrCreate))
        {
            VersionFile localVersionFile=new VersionFile();
            StreamReader streamReader=new StreamReader(fileStream);
            string versionLine= streamReader.ReadLine();
            if (int.TryParse(versionLine,out version))
            {
                LogTool.Log($"old version={version},new version={version+1}");
            }
            else
            {
                LogTool.LogError("读取版本号失败,原因可能是第一次创建版本文件");
            }

            localVersionFile.version = version+1;
            foreach (var dep in fileHashList)
            {
                var fileInfo = new FileInfo(string.Format("{0}/{1}", PathUtility.ProjectBuildABPath, dep.Key));
                localVersionFile.AddBundleVerison(dep.Key, dep.Value, (ulong)fileInfo.Length);
            }
            
            
            fileStream.SetLength(0);
            StreamWriter streamWriter=new StreamWriter(fileStream);
            localVersionFile.Save(streamWriter);
            
            streamWriter.Flush();
            streamWriter.Close();
        }
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
