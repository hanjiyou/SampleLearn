using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListSort : MonoBehaviour
{
    private List<StudentSort1> _studentSort1s;
    // Start is called before the first frame update
    void Start()
    {
        InitStudentsData();
        _studentSort1s.Sort(StudentListSort);
        LogList(_studentSort1s);
    }

    void InitStudentsData()
    {
        _studentSort1s=new List<StudentSort1>();
        _studentSort1s.Add(new StudentSort1(){Name ="han4",Age=5,status = 1});

        _studentSort1s.Add(new StudentSort1(){Name ="han1",Age=5,status = 0});

        _studentSort1s.Add(new StudentSort1(){Name ="han2",Age=5,status = 1});

        _studentSort1s.Add(new StudentSort1(){Name ="han4",Age=5,status = 3});
        _studentSort1s.Add(new StudentSort1(){Name ="han3",Age=5,status = 2});

        
        _studentSort1s.Add(new StudentSort1(){Name ="han4",Age=5,status = 0});
        _studentSort1s.Add(new StudentSort1(){Name ="han4",Age=5,status = 3});
    }
    /// <summary>
    /// 返回值为1 表示交换 -1表示不交换 0表示没有前后关系
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    int StudentListSort(StudentSort1 s1,StudentSort1 s2)
    {
        int result=0;
        int index1 = MapWight(s1.status);
        int index2 = MapWight(s2.status);
        if (index1 > index2)
        {
            result = 1;
        }else if (index1 < index2)
        {
            result = -1;
        }
        else
        {
            result = 0;
        }
       

        return result;
    }

    int MapWight(int status)
    {
        int index=0;
        switch (status)
        {
            case 3:
                index = 0;
                break;
            case 0:
                index = 1;
                break;
            case 2:
                index = 2;
                break;
            case 1:
                index = 3;
                break;
        }

        return index;
    }
    void LogList(List<StudentSort1> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Loger.Log($"第{i}个="+list[i].ToString());
        }
    }
}

public class StudentSort1
{
    public string Name;
    public int Age;
    public int status;
    public override string ToString()
    {
        return "Name="+Name+",Age="+Age+",Status="+status;
    }
}