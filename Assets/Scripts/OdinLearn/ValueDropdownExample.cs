using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class ValueDropdownExample : MonoBehaviour
{

    [ValueDropdown("DropDown1IntegerArray")]
    public int DropDown1IntegerField = 5;
    private int[] DropDown1IntegerArray = {1, 2, 3, 4, 5};

    [OnValueChanged("OnDictValueChange")]
    [ValueDropdown("DropDown2Dict")]
    public int DropDown2DictField;
    private IEnumerable DropDown2Dict = new ValueDropdownList<int>()
    {
        {"two",2},
        {"one", 1},
    };

    void OnDictValueChange()
    {
        Debug.Log("onchanged="+DropDown2DictField);
    }
    //SortDropdownItems默认为false(不排序) 开启后为下拉列表为根据Key升序排序
    [ValueDropdown("DropDown3SortList",SortDropdownItems = true)]
    public int DropDown3SortField;
    private IEnumerable DropDown3SortList=new ValueDropdownList<int>()
    {
        {"one",2},
        {"two",1},
        {"three",3},
        {"four",4}
    };
    [ValueDropdown("DropDown3SortList",DropdownTitle = "下拉框标题",DropdownHeight =110,DropdownWidth = 220)]
    public int Dropdown4TitleAndHeightAndWidth;
    
//树形图结构
    [ValueDropdown("DropDown4NoTree",FlattenTreeView =true)]
    public float DropDown4NoTreeField;

    private IEnumerable DropDown4NoTree=new ValueDropdownList<float>()
    {
        {"one/1",1.1f},
        {"one/2",1.2f},
        {"two/1",2.1f},
        {"two/2",2.2f},
        {"two/3",2.3f},
    };
    
    [ValueDropdown("DropDown4HaveTree",FlattenTreeView =false)]
    public float DropDown4HaveTreeField;
    private IEnumerable DropDown4HaveTree=new ValueDropdownList<float>()
    {
        {"one/1",1.1f},
        {"one/2",1.2f},
        {"two/1",2.1f},
        {"two/2",2.2f},
        {"two/3",2.3f},
    };
//勾选框
    [ValueDropdown("GetAllSceneObjects")]
    public List<GameObject> GameobjectListManyField;
    private static IEnumerable GetAllSceneObjects()
    {
        Func<Transform, string> getPath = null;
        getPath = x => (x ? getPath(x.parent) + "/" + x.gameObject.name : "");//三元运算符 其中X为Transform
        return GameObject.FindObjectsOfType<GameObject>().Select(x =>
            {
                return new ValueDropdownItem(getPath(x.transform), x);
            });
    }
    
    [ValueDropdown("GetAllSceneObjects",IsUniqueList = true)]
    public List<GameObject> GameobjectListUniqueField;
}