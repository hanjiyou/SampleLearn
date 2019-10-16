using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HideInTablesAttributeExample : MonoBehaviour
{
    
    public Student student=new Student();
    
    [ShowInInspector]
    [TableList]
    public List<Student> Students=new List<Student>()
    {
        new Student(),
        new Student()
    };
    
    [Serializable]
    public class Student
    {
        public int Age;
        public int Name;
        [HideInTables]
        public string Id;
    }
    
    [HideReferenceObjectPicker]
    [ShowInInspector]
    public A a=new A();
    
   public class A
   {
       
   }
}
