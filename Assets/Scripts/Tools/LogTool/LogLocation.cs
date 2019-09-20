﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
public class LogLocation
{
    private static LogLocation m_Instance;
    public static LogLocation GetInstacne()
    {
        if (m_Instance == null)
        {
            m_Instance = new LogLocation();
        }
        return m_Instance;
    }
    private const string DEBUGERFILEPATH = "Assets/Scripts/Tools/LogTool/Loger.cs";//替换成你自己的封装类地址
    private int m_DebugerFileInstanceId;
    private Type m_ConsoleWindowType = null;
    private FieldInfo m_ActiveTextInfo;
    private FieldInfo m_ConsoleWindowFileInfo;

    private LogLocation()
    {
        UnityEngine.Object debuggerFile = AssetDatabase.LoadAssetAtPath(DEBUGERFILEPATH, typeof(UnityEngine.Object));
        m_DebugerFileInstanceId = debuggerFile.GetInstanceID();
        m_ConsoleWindowType = Type.GetType("UnityEditor.ConsoleWindow,UnityEditor");
        m_ActiveTextInfo = m_ConsoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
        m_ConsoleWindowFileInfo = m_ConsoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
    }

    [UnityEditor.Callbacks.OnOpenAssetAttribute(-1)]
    private static bool OnOpenAsset(int instanceID, int line)
    {
        if (instanceID == LogLocation.GetInstacne().m_DebugerFileInstanceId)
        {
            return LogLocation.GetInstacne().FindCode();
        }
        return false;
    }

    public bool FindCode()
    {
        var windowInstance = m_ConsoleWindowFileInfo.GetValue(null);
        var activeText = m_ActiveTextInfo.GetValue(windowInstance);
        string[] contentStrings = activeText.ToString().Split('\n');
        List<string> filePath = new List<string>();
        for (int index = 0; index < contentStrings.Length; index++)
        {
            if (contentStrings[index].Contains("at"))
            {
                filePath.Add(contentStrings[index]);
            }
        }
        bool success = PingAndOpen(filePath[1]);
        return success;
    }


    public bool PingAndOpen(string fileContext)
    {
        string regexRule = @"at ([\w\W]*):(\d+)\)";
        Match match = Regex.Match(fileContext, regexRule);
        if (match.Groups.Count > 1)
        {
            string path = match.Groups[1].Value;
            string line = match.Groups[2].Value;
            UnityEngine.Object codeObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
            if (codeObject == null)
            {
                return false;
            }
            EditorGUIUtility.PingObject(codeObject);
            AssetDatabase.OpenAsset(codeObject, int.Parse(line));
            return true;
        }
        return false;
    }
}
