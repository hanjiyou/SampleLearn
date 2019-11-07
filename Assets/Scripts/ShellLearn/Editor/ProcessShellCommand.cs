using System.Diagnostics;
using UnityEditor;
public  class ProcessShellCommand
{
    private static string QQRootPath = "D:\\PlaySoftwareInstall\\QQ\\Bin\\";
    [MenuItem("MyTools/LaunchQQ")]
    public static void LaunchApplication()
    {
        ProcessCommand(QQRootPath+"QQ","");
    }
    
    [MenuItem("MyTools/Git/Status")]
    public static void GitStatus()
    {
        ProcessCommand("git","status");
    }
    [MenuItem("MyTools/Git/Add")]
    public static void GitAdd()
    {
        ProcessCommand("git","add *");
    }
//    private static string GitRootPath = "D:\\WorkSoftwareInstall\\Git\\bin";
    [MenuItem("MyTools/Git/Push")]
    public static void GitPush()
    {
        ProcessCommand("git","push");
    }

    [MenuItem("MyTools/Git/Pull")]
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
            Loger.Log("output="+p.StandardOutput.ReadToEnd());
            Loger.Log("errorLog="+p.StandardError.ReadToEnd());
        }
        p.WaitForExit();
        p.Close();
    }

 
}
