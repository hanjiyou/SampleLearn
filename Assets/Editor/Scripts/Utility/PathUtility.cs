using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 路径工具
/// </summary>
public class PathUtility
{
   public readonly static string AssetDataPath = Application.dataPath;
   public readonly static string StreamingAssetsPath = Application.streamingAssetsPath;
   
   public readonly static string ProjectBuildABPath = "D:/Windows/SampleLearn/AssetBundles";
   public const string AssetBundlePathName = "AssetBundles";
   public const string ResourcePathName="Resources";
   public const string VersionFileName = "version.txt";
   public const string ABSuffix = ".ab";
   public static string GetTargetPlatformPath()
   {
#if UNITY_EDITOR
      return GetTargetPlatformPath(EditorUserBuildSettings.activeBuildTarget);
#else
      return GetTargetPlatformPath(Application.platform);
#endif
   }

   /// <summary>
   /// 反转反斜杠
   /// </summary>
   /// <returns></returns>
   public static string RevertPathBackslash(string sourcePath)
   {
       string newPath = sourcePath.Replace('\\', '/');
       return newPath;
   }

   /// <summary>
   /// 合并路径
   /// </summary>
   /// <param name="path1"></param>
   /// <param name="path2"></param>
   /// <returns></returns>
   public static string CombinePath(string path1, string path2)
   {
       return Path.Combine(path1, path2).Replace('\\','/');
   }
   #region 内部功能方法

   /// <summary>
   /// 编辑器模式下的各平台路径
   /// </summary>
   /// <param name="buildTarget"></param>
   /// <returns></returns>
   private static string GetTargetPlatformPath(BuildTarget buildTarget)
   {
       string platformPath = string.Empty;
       switch (buildTarget)
       {
           case BuildTarget.Android:
               platformPath = "Android";
               break;
           case BuildTarget.iOS:
               platformPath = "Ios";
               break;
           case BuildTarget.StandaloneWindows:
           case BuildTarget.StandaloneWindows64:
               platformPath = "Windows";
               break;
       }
       return platformPath;
   }
   /// <summary>
   /// 打包运行各个平台下的路径
   /// </summary>
   /// <param name="runtimePlatform"></param>
   /// <returns></returns>
   private static string GetTargetPlatformPath(RuntimePlatform runtimePlatform)
   {
       string platformPath = string.Empty;
       switch (runtimePlatform)
       {
           case RuntimePlatform.Android:
               platformPath = "Android";
               break;
           case RuntimePlatform.IPhonePlayer:
               platformPath = "Ios";
               break;
           case RuntimePlatform.WindowsPlayer:
               platformPath = "Windows";
               break;
           case RuntimePlatform.OSXPlayer:
               platformPath= "OSX";
               break;
       }
       return platformPath;
   }

   #endregion
   
}
