using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XLua;
/// <summary>
/// C#访问lua（全局类型的数据）
/// 1.基本类型的数据 直接通过 luaEnv.Global.Get<int>()获取
/// 2.table类型的数据
/// (1)值拷贝
/// ①映射到普通的class，有无参构造构造和对应的public属性即可，table的属性可以多于或者少于class的属性，这个过程是值拷贝，
/// 如果class比较复杂代价会比较大。而且修改class的字段值不会同步到table，反过来也不会。
/// ②映射到struct
/// (2)引用拷贝(建议使用)
/// ①映射到interface（依赖于生成代码，否则抛InvalidCastException异常）代码生成器会生成该接口的实例，如果get一个属性，
/// 生成代码会get对应的table，set同。即修改值，会相互影响。甚至可以访问lua函数
/// (3)轻量级的方式
/// ①byValue映射(值拷贝)：映射到Dictionary<>,List<>
/// 前提table下key和value的类型都是一致的
/// ②by ref
/// 本质是映射到luaTable。好处是不需要生成代码，坏处是速度慢（比interface慢一个量级），且没有类型检查
/// 3. 访问一个全局的function（仍然是get方法，不同的是类型映射）
/// ①映射到delegate（建议使用）
/// 性能好很多，类型安全，唯一缺点是需要生成代码(即设置3种方式的配置)
/// ②映射到luaFunction
/// 优缺点与映射到委托相反。
/// </summary>
public class CSharpCallLuaDemo02 : MonoBehaviour
{
    private LuaEnv _luaEnv;
    private string _luaScript = @"
    a=1
    b='hello world'
    c=true
    d=1.1
    valueClass1={
        f1=1,f2=10,
        add=function(self,a,b)
            print('valueClass1.add called')
            return a+b
        end
    }
    byValueListTable={'1','a','战斗'}
    byValueDictTable={one=1,two=2}
    function action()
        print('this is action')
    end
    function map2Delegate(a,b,c)
         print('a',a,'b=',b)
        c=1.111
        return 'i am map2delegate',{f1=1}
    end

    ";
    private void Awake()
    {
        _luaEnv=new LuaEnv();
    }
    /// <summary>
    /// lua表的映射类
    /// </summary>
    public class ValueClass1
    {
        public int f1;
        public int f2;
        
        public override string ToString()
        {
            return "f1=" + f1 + ",f2=" + f2;
        }
    }
    /// <summary>
    /// lua表的映射接口(引用映射)
    /// </summary>
    [CSharpCallLua]//打标签的方式，配置白名单，生成适配代码
    public interface ILuaMap1
    {
        int f1 { get; set; }
        int f2 { get; set; }
        int add(int a, int b);
    }
    [CSharpCallLua]
    public delegate string MapDelegate(int a, bool b, out ValueClass1 c);
    // Start is called before the first frame update
    void Start()
    {
        _luaEnv.DoString(_luaScript);
        LogTool.Log($"luaA={_luaEnv.Global.Get<int>("a")}");
        LogTool.Log($"luaB={_luaEnv.Global.Get<string>("b")}");
        LogTool.Log($"luaC={_luaEnv.Global.Get<bool>("c")}");
        LogTool.Log($"luaD={_luaEnv.Global.Get<float>("d")}");
        //映射到class 值拷贝 发生修改互相不同步
        LogTool.Log("lua ValueClass1=",_luaEnv.Global.Get<ValueClass1>("valueClass1"));
        //映射到接扣 引用拷贝 
        ILuaMap1 luaMap1 = _luaEnv.Global.Get<ILuaMap1>("valueClass1");
        LogTool.Log("lua映射到接口 调用方法 结果=", luaMap1.add(luaMap1.f1, luaMap1.f2));
        luaMap1.f2 = 9;
        _luaEnv.DoString("print(valueClass1.f2)");
        List<string> list = _luaEnv.Global.Get<List<string>>("byValueListTable");
        for (int i = 0; i < list.Count; i++)
        {
            LogTool.Log($"{i}=",list[i]);
        }
        Dictionary<string, int> dict = _luaEnv.Global.Get<Dictionary<string, int>>("byValueDictTable");
        LogTool.Log("dict.lenght="+dict.Count);
        for (int i = 0; i < dict.Count; i++)
        {
            LogTool.Log($"{i}=",dict.ElementAt(i));
        }
        _luaEnv.DoString("byValueDictTable['one']=3");
        dict = _luaEnv.Global.Get<Dictionary<string, int>>("byValueDictTable");
        for (int i = 0; i < dict.Count; i++)
        {
            LogTool.Log($"{i}=",dict.ElementAt(i));
        }
        LuaTable luaTable = _luaEnv.Global.Get<LuaTable>("valueClass1");//映射到luaTable by ref 
        LogTool.Log("f2=",luaTable.Get<int>("f2"));
//映射到delegate
    //lua多返回值的映射 从左到右填充
        MapDelegate mapDelegate = _luaEnv.Global.Get<MapDelegate>("map2Delegate");
        ValueClass1 c;
        string result=mapDelegate(10, true, out c);
        LogTool.Log("result="+result+"c="+c);
    //系统委托的映射
        Action action = _luaEnv.Global.Get<Action>("action");
        action();
//映射到LuaFunction
        LuaFunction luaFunction = _luaEnv.Global.Get<LuaFunction>("action");
        luaFunction.Call();
        luaFunction = _luaEnv.Global.Get<LuaFunction>("map2Delegate");
        ValueClass1 valueClass1=null;
        object []a= luaFunction.Call(1,false,valueClass1);
        LogTool.Log("a="+a[0]+"c="+c);
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private void OnDestroy()
    {
        _luaEnv.Dispose();
    }
}
