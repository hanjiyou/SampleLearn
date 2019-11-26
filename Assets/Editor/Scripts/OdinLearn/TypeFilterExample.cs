using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 类型过滤器案例
/// </summary>
public class TypeFilterExample : MonoBehaviour
{
    [ShowInInspector]
    public BaseClass ANoFilter;

    [ShowInInspector]
    [TypeFilter("FilterGeneric")]
    public BaseClass AFilterGeneric;
    
    [ShowInInspector,TypeFilter("FilterSon")]
    public BaseClass AFilterSon;
    
    [ShowInInspector]
    public TestTypeSer C;

    IEnumerable<Type> FilterGeneric()
    {
        var q = typeof(BaseClass).Assembly.GetTypes()
            .Where(x=>x.IsGenericType);

        return q;
    }


    IEnumerable<Type> FilterSon()
    {
        var q = typeof(BaseClass).Assembly.GetTypes()
            .Where(x=>typeof(BaseClass).IsAssignableFrom(x))
            .Where(x=>typeof(BaseClass)!=x);
        return q;
    }
    
    
    
    
    
    
    public abstract class BaseClass
    {
        public int BaseField;
    }
    public class A1 : BaseClass { public int _A1; }
    public class A2 : A1 { public int _A2; }
    public class A3 : A2 { public int _A3; }
    public class B1 : BaseClass { public int _B1; }
    public class B2 : B1 { public int _B2; }
    public class B3 : B2 { public int _B3; }
    public class C1<T> : BaseClass { public T C; }
}
public class TestTypeSer
{
    public int Age;
}