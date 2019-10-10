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
    public GameObject DefaultGO;
    
    [CustomValueDrawer("HaveLabelNameFunction")]
    public string HaveLabelName;
    [CustomValueDrawer("NoLabelNameFunction")]
    public string NoLabelName;

    public string HaveLabelNameFunction(string tempName, GUIContent label)
    {
        return EditorGUILayout.TextField(label,"韩"+tempName);
    }
    public string NoLabelNameFunction(string tempName, GUIContent label)
    {
        return EditorGUILayout.TextField(label,tempName);
    }
}
