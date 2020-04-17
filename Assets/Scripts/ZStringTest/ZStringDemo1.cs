using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
/// <summary>
/// 0GC库测试
/// </summary>
public class ZStringDemo1 : MonoBehaviour
{
    public Text text1;
    public Text text2;
    private int num = 12354;
    void Start()
    {
//第一章 字符串现状
    //1.常量字符串会自动如此 字面值以及使用+连接的字面值会被自动加入到缓存池，以保证相同内容的常量只存在一个实例
//        string a = "hello world";//字面量自动入池
//        string b = "hello world";
//        string c = "hello"+" world";
//        string d = "hello ";
//        string e = d+"world";//新对象
//        LogTool.Log("a=b?"+ReferenceEquals(a,b));
//        LogTool.Log("a=c?"+object.ReferenceEquals(a,c));
//        LogTool.Log("a=e?"+object.ReferenceEquals(a,e));
//        LogTool.Log("a==e?"+(a==e));
//    //2.Intern与IsInterned
//        string a2=new string(new char[]{'a','b'});//不是字面量 没入池
//        string copyA2 = string.Copy(a2);
//        LogTool.Log("isinterned111"+string.IsInterned(copyA2));//未入池 返回null
//
//        LogTool.Log("a2=copy?"+ReferenceEquals(a2,copyA2));
//        string.Intern(copyA2);
//        LogTool.Log("isinterned222"+string.IsInterned(copyA2));//已入池 返回引用
//
//        LogTool.Log("a2=copy.intern?"+ReferenceEquals(copyA2,string.Intern(a2)));
//        LogTool.Log("a2=copy?"+ReferenceEquals(copyA2,a2));
//第二章 优化方向
        string two1 = "hello world";
        Change(two1,'x');
        LogTool.Log("hhh two1="+two1);
        LogTool.Log("hello world");//xxxxx 因为已经Intern的字符串，C#底层会根据字面值在内存中寻找真实值value
        LogTool.Log("hello");
        
    }

    private void Update()
    {
        int num = 5;
        Profiler.BeginSample("test1");
        text1.text = num.ToString();//拆装箱 会产生GC
//        for (int i = 0; i < 1000; i++)
//        {
//            string c = "str1" +"str2";//变量拼接 每次都会产生新对象 产生很多GC
//        }
        Profiler.EndSample();
        
        Profiler.BeginSample("test2");
//zstring使用demo(不可作为类成员变量，不建议using中写超大for循环)
        //短周期字符串 直接使用
        using (zstring.Block())
        {
            zstring zstr = num.ToString("00");
            text2.text = zstr;
//            zstring zstr = "zstr1";
//            zstring zstr2 = "zstr2";
//            text2.text = zstr+zstr2;
        }
        //长周期字符串（如资源路径）
//        using (zstring.Block())
//        {
//            string a = "Assets/";
//            zstring b = a + "prefabs/" + "solider.prefab";
////            a.path = b.Intern();
//        }
        Profiler.EndSample();
    }

    unsafe void Change(string a, char b)
    {
        fixed (char* s = a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                s[i] = b;
            }
        }
    }
}
