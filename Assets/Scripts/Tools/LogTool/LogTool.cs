using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogTool
{
    public static void Log(string content)
    {
        Debug.Log(content);
    }
    public static void Log(string content,System.Object obj)
    {
        Debug.Log(content+obj);
    }

    public static void LogWarning(string content)
    {
        Debug.LogWarning(content);
    }
    public static void LogWarning(string content,System.Object obj)
    {
        Debug.LogWarning(content+obj);
    }

    public static void LogError(string content)
    {
        Debug.LogError(content);
    }
    public static void LogError(string content,System.Object obj)
    {
        Debug.LogError(content+obj);
    }
}
