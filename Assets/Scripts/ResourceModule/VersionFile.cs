using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VersionFile
{
    public int version;
    public Dictionary<string,BundleInfo> bundleVersion=new Dictionary<string,BundleInfo>();
    
    public void AddBundleVerison(string key, string hash, ulong size)
    {
        bundleVersion[key] = new BundleInfo(hash, size);
    }
    
    /// <summary>
    /// 保存版本文件流中的一切信息，包括版本号、Bundle信息
    /// </summary>
    /// <param name="sw"></param>
    public void Save(StreamWriter sw)
    {
        sw.Write(version);
        sw.Write('\n');
        var enumer = bundleVersion.GetEnumerator();
        while (enumer.MoveNext())
        {
            var sss = string.Format("{0}|{1}|{2}\n", enumer.Current.Key, enumer.Current.Value.Md5, enumer.Current.Value.Size);
            sw.Write(sss);
        }
    }
    
    public bool Load(StreamReader sr, out string error)
    {
        var content = sr.ReadToEnd();
        return Load(content, out error);
    }

    public bool Load(string content, out string error)
    {
        var lineList = content.Split('\n');
        if (lineList.Length <= 0)
        {
            error = "文件格式无效";
            return false;
        }
        if (!int.TryParse(lineList[0], out version))
        {
            error = "无效的version号";
            return false;
        }
        for (int i = 1; i < lineList.Length; i++)
        {
            var line = lineList[i];
            if (line == "") continue;
            var pair = line.Split('|');
            if (pair.Length == 3)
            {
                bundleVersion[pair[0]] = new BundleInfo(pair[1], ulong.Parse(pair[2]));
            }
            else
            {
                error = string.Format("无效的BundleInfo[{0}]", line);
                return false;
            }
        }
        error = "";
        return true;
    }
}

public struct BundleInfo
{
    public string Md5;
    public ulong Size;

    public BundleInfo(string md5, ulong size)
    {
        Md5 = md5;
        Size = size;
    }
}
