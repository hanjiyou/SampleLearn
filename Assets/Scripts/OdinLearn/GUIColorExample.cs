using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GUIColorExample : MonoBehaviour
{
    [Required]
    public string color0;
    
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public int ColoredInt1;

    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public int ColoredInt2;

    [ButtonGroup("按钮组1")]
    [GUIColor(0, 1, 0)]
    private void Apply()
    {
        Debug.Log("应用 color0="+color0);
    }

    [ButtonGroup]
    [GUIColor(1, 0.6f, 0.4f)]
    private void Cancel()
    {
        Debug.Log("取消");
    }
    [ButtonGroup]
    [GUIColor(1, 0.6f, 0.4f)]
    private void Cancel2()
    {
        Debug.Log("取消2");
    }
//按钮动态颜色
    //方法1
    [GUIColor("GetButtonColor")]//指定颜色渐变函数
    [Button(ButtonSizes.Gigantic)]//大按钮
    private static void IAmFabulous()
    {
    }
    /// <summary>
    /// GUIColor指定的颜色渐变函数(会执行很多次)
    /// </summary>
    /// <returns></returns>
    private static Color GetButtonColor()
    {
//        Debug.Log("GetButtonColor");

        Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
        return Color.HSVToRGB(Mathf.Cos((float)UnityEditor.EditorApplication.timeSinceStartup + 1f) * 0.225f + 0.325f, 1, 1);
    }
    //方法2
    // [GUIColor("@Color.Lerp(Color.red, Color.green, Mathf.Sin((float)EditorApplication.timeSinceStartup))")]
    // [GUIColor("CustomColor")]
    // 这两个写法相等
    [Button(ButtonSizes.Large)]
//    [GUIColor("Color.Lerp(Color.red, Color.green, Mathf.Sin((float)EditorApplication.timeSinceStartup))")]
    private static void Expressive_One()
    {
    }
    [Button(ButtonSizes.Large)]
    [GUIColor("CustomColor")]
    private static void Expressive_Two()
    {
    }

# if UNITY_EDITOR
    public Color CustomColor()
    {
        return Color.Lerp(Color.red, Color.green, Mathf.Sin((float)EditorApplication.timeSinceStartup));
    }
# endif
}
