using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
/// <summary>
/// 资源文件工具
/// </summary>
public class ResourceUtility
{
    public static string GetMD5(string filePath)
    {
        StringBuilder md5SB=new StringBuilder();
        using (FileStream fileStream=new FileStream(filePath,FileMode.Open))
        {
            MD5 md5=new MD5CryptoServiceProvider();
            byte[] md5BytesArr= md5.ComputeHash(fileStream);

            for (int i = 0; i < md5BytesArr.Length; i++)
            {
                md5SB.Append(md5BytesArr[i].ToString("x2"));//转化为小写的16进制 X2:转化为大写的16进制
            }
        }
        return md5SB.ToString();
    }

    public static string GetMD5(Stream fileStream)
    {
        StringBuilder md5SB=new StringBuilder();
        MD5 md5=new MD5CryptoServiceProvider();
        byte[] md5BytesArr = md5.ComputeHash(fileStream);
        for (int i = 0; i < md5BytesArr.Length; i++)
        {
            md5SB.Append(md5BytesArr[i].ToString("x2"));
        }

        return md5SB.ToString();
    }
}
