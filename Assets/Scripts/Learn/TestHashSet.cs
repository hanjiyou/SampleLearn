using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHashSet : MonoBehaviour
{
    HashSet<int> _hashSet1=new HashSet<int>();
    HashSet<int> _hashSet2=new HashSet<int>();

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            _hashSet1.Add(i);
        }

        for (int i = 3; i < 10; i++)
        {
            _hashSet2.Add(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
//        _hashSet1.ExceptWith(_hashSet2);//ExceptWith：提出原集中所有参数集存在的元素  非集
//        _hashSet1.UnionWith(_hashSet2);//UnionWith:合并集合1和集合2  并集
        _hashSet1.IntersectWith(_hashSet2);//IntersectWith:求集合的交集
        foreach (var item in _hashSet1)
        {
            LogTool.Log($"item={item}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
