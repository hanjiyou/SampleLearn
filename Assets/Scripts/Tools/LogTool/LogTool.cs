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

    public static void LogWarring(string content)
    {
        Debug.Log(content);
    }
    public static void LogWarring(string content,System.Object obj)
    {
        Debug.Log(content+obj);
    }
}
