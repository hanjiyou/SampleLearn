using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
/// <summary>
/// CustomValueDrawer 自定义字段绘制方法 参数:绘制方法名
/// </summary>
public class CustomValueDrawerExample : MonoBehaviour
{
//绘制TextFiled
    [ShowInInspector]
    [CustomValueDrawer("HaveLabelNameFunction")]
    public string HaveLabelName { get; set; }

    [CustomValueDrawer("NoLabelNameFunction")]
    public string NoLabelName;
    /// <summary>
    /// 自定义字段绘制方法
    /// </summary>
    /// <param name="tempName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public string HaveLabelNameFunction(string tempName, GUIContent label)
    {
        return EditorGUILayout.TextField(label,tempName);//对应字段使用文本+输入框的方式显示
    }
    public string NoLabelNameFunction(string tempName, GUIContent label)
    {
        return EditorGUILayout.TextField(tempName);//对应字段使用输入框的方式显示
    }
//绘制Slider
    public float Max = 100, Min = 0;
    [CustomValueDrawer("CustomDrawerStaticMethod")]
    public float CustomDrawerStatic;
    /// <summary>
    /// 绘制范围固定的slider
    /// </summary>
    /// <param name="value"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public  float CustomDrawerStaticMethod(float value,GUIContent label)
    {
        return EditorGUILayout.Slider(label,value,0f, 10f);
    }
    
    [CustomValueDrawer("CustomDrawerInstanceMethod")]
    public float CustomDrawerInstance;
    /// <summary>
    /// 绘制范围是变量的Slider
    /// </summary>
    /// <param name="value"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public float CustomDrawerInstanceMethod(float value,GUIContent label)
    {
        return EditorGUILayout.Slider(label,value,Min, Max);
    }
    [CustomValueDrawer("CustomDrawerArrayMethod")]
    public float[] CustomDrawerArray = new float[] {11, 22, 33};
    /// <summary>
    /// 绘制数组的slider
    /// </summary>
    /// <param name="value"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public float CustomDrawerArrayMethod(float value,GUIContent label)
    {
        return EditorGUILayout.Slider(value, Min, Max);
    }
}
