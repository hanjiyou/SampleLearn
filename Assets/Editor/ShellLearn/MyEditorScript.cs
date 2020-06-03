using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MyEditorScript
{
   public static void RunTest()
   {
       Debug.Log("Hello, I am Unity, I am ok");
       string[] recArgs = GetCommandLineArgs();
       for (int i = 0; i < recArgs.Length; i++)
       {
           LogTool.Log($"第{i}个参数={recArgs[i]}");
       }
   }

   private static string[] GetCommandLineArgs()
   {
       return Environment.GetCommandLineArgs();
   }

   public static void BuildAndroid()
   {
       BuildReport error = BuildPipeline.BuildPlayer(new []{"Assets/Scenes/TestGenerateMesh.unity"}, "test.apk", BuildTarget.Android, BuildOptions.None);
   }
}
