using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GetPrefabScriptTool
{
    [MenuItem("MyTools/GetPrefabScript")]
    public static void GetPrefabScript()
    {
        GetPrefabAttachScripts();
    }

    private static void GetPrefabAttachScripts()
    {
        EditorUtility.DisplayProgressBar("Progress", "Find Class...", 0);
        string[] prefabPath = {"Assets/Resources"};
        string[] assetGuids= AssetDatabase.FindAssets("t:prefab",prefabPath);
        int count = 0;
        for (int i = 0; i < assetGuids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
            LogTool.Log("hhh "+assetPath);
            GameObject gameObject= AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            Component[] components = gameObject.transform.GetComponents<Component>();
            for (int j = 0; j < components.Length; j++)
            {
                string fullName = components[j].GetType().FullName;
                LogTool.Log("hhh fullName="+fullName);
            }

            count++;
            EditorUtility.DisplayProgressBar("Progress", "Find Class...", count / assetGuids.Length);
        }
        EditorUtility.ClearProgressBar();
    }
}
