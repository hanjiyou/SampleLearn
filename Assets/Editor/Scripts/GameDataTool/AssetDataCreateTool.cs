using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetDataCreateTool
{
    private static string DataRootPath = "Assets/Resources/GameData/";
    [MenuItem("MyTools/GameData/GenerateSystemConfig")]
    public static void GenerateSystemConfigAsset()
    {
        string path = DataRootPath+"NoUpdate/";
        GenerateAsset<SystemConfig>(path);
    }

    private static void GenerateAsset<T>(string dirPath) where T:ScriptableObject
    {
        if (!Directory.Exists (dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        string path =dirPath + typeof(T)+ ".asset";//使用类  的名字
        ScriptableObject ob = ScriptableObject.CreateInstance(typeof(T));
        if (ob ==null)
        {
            LogTool.LogError("cant creat file：" + typeof(T));
        }
        else
        {
            LogTool.Log("Creat path:" +path);
            AssetDatabase.CreateAsset(ob, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
    }
}
