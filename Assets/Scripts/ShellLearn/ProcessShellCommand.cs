using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class ProcessShellCommand : MonoBehaviour
{
    private static string GitRootPath = "D:\\WorkSoftwareInstall\\Git\\bin";
    [MenuItem("MyTools/GitUpdate")]
    public static void GitUpdate()
    {
        ProcessCommand("git","pull");
    }
    
    /// <summary>
    /// unity内部执行外部exe或者shell命令
    /// </summary>
    /// <param name="command"></param>
    /// <param name="argument"></param>
    private static void ProcessCommand(string command, string argument)
    {
        ProcessStartInfo startInfo=new ProcessStartInfo(command);
        startInfo.Arguments = argument;
        startInfo.CreateNoWindow = false;
        startInfo.ErrorDialog = false;
        startInfo.UseShellExecute = false;
        if (startInfo.UseShellExecute)
        {
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            startInfo.RedirectStandardInput = false;
        }
        else
        {
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
            startInfo.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
        }
        Process p = Process.Start(startInfo);
        {
            Loger.Log(p.StandardOutput.ReadToEnd());
            Loger.Log(p.StandardError.ReadToEnd());
        }
        p.WaitForExit();
        p.Close();
    }

    private static void LaunchApplication(string path)
    {
        
    }
}
