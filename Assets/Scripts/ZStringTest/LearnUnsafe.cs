using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// unsafe指针
/// </summary>
public class LearnUnsafe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
            int [] a=new int[1];
            a[0] = 12;
            int b = 2;
            Add(a, b);
            LogTool.Log("hhh a="+a[0]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    unsafe void Add(int[] arr,int b)
    {
        fixed (int* p = arr)
        {
            *p= *p + b;

        }
    }
}
