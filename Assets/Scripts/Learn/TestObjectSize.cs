using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;


public class TestObjectSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Student student=new Student(){Age=1};
        long size = 0;
        using (Stream stream=new MemoryStream())
        {
            BinaryFormatter binaryFormatter=new BinaryFormatter();
            binaryFormatter.Serialize(stream,student);//将对象序列化到流，根据流获取对象长度
            size = stream.Length;
        }
        LogTool.Log("student.length="+size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class Student
{
    public int Age;
}