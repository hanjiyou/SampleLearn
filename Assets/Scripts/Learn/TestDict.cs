using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public enum DictEnum
{
    One,
    Two,
    Three
}
public class TestDict : MonoBehaviour
{
    Dictionary<DictEnum,int> myEnumDic=new Dictionary<DictEnum, int>();//枚举做字典的key 在添加时会发生装箱操作(使用默认的Comparer)
                                                                       //,对于未实现IEquatable的枚举，在调用Equals比较时 会转换成Object产生装箱操作
                                                                       //因此，解决方案就是自己实现一个Comparer传递给枚举key字典
                                                                       
    // Start is called before the first frame update
    void Start()
    {
        Profiler.BeginSample("test foreach start");

        myEnumDic.Add(DictEnum.One,1);
        myEnumDic.Add(DictEnum.Two,1);
        myEnumDic.Add(DictEnum.Three,1);
        Profiler.EndSample();
    }

    // Update is called once per frame
    void Update()
    {
        Profiler.BeginSample("test foreach update");
        foreach (var item in myEnumDic)
        {
//            Debug.Log("hhh"+item.Value);
        }
        Profiler.EndSample();
    }
}

public class EnumComparer : IEqualityComparer<DictEnum>
{
    public bool Equals(DictEnum x, DictEnum y)
    {
        return (int) x == (int) y;
    }

    public int GetHashCode(DictEnum obj)
    {
        return (int) obj;
    }
}