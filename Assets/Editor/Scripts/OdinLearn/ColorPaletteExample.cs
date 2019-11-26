using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ColorPaletteExample : MonoBehaviour
{
    [ColorPalette]
    public Color ColorOptions;
    
    [PropertySpace(50)]
    [ColorPalette("MyUnderwater")]
    public Color UnderwaterColor;
    
    
    [FoldoutGroup("Color Paletteshhh",true)]
    [ListDrawerSettings(IsReadOnly = true)]
    [PropertyOrder(9)]
    public List<ColorPalette> ColorPalettes;

#if UNITY_EDITOR

    [FoldoutGroup("Color Paletteshhh"), Button("获取调色板",ButtonSizes.Large), GUIColor(0, 1, 0), PropertyOrder(8)]
    private void FetchColorPalettes()
    {
        this.ColorPalettes = Sirenix.OdinInspector.Editor.ColorPaletteManager.Instance.ColorPalettes
            .Select(x => new ColorPalette()
            {
                Name = x.Name,
                Colors = x.Colors.ToArray()
            })
            .ToList();
    }

#endif
    [Serializable]
    public class ColorPalette
    {
        [HideInInspector]
        public string Name;

        [LabelText("$Name")]
        [ListDrawerSettings(IsReadOnly = true, Expanded = false)]//IsReadOnly是否可以删除序列化数组  Expanded覆盖默认设置，是否展开，false为不展开状态
        public Color[] Colors;
    }
    
    [GUIColor(1,1,0,1)]
    [PropertyOrder(10)]
    [Button("mybtn", ButtonSizes.Large)]
    void ExecuteBtn(string str)
    {
        LogTool.Log("hhh 执行按钮点击方法str="+str);
    }
    
    [field:SerializeField]
    public int A { get; set; }
    
    [SerializeField]
    public int B { get; set; }
}
