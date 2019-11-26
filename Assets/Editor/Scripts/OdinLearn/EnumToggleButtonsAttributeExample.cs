using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnumToggleButtonsAttributeExample : MonoBehaviour
{
    [Title("Default")]
    public SomeBitmaskEnum DefaultEnum;

    [Title("Standard Enum")]
    [EnumToggleButtons]
    public SomeEnum SomeEnumField;
    
    [Title("StandardEnum")] 
    [EnumToggleButtons]
    public SomeBitmaskEnum EnumToggleButtons;
    
    public enum SomeEnum
    {
        One,Two,Three,Four,Five,Six,Seven
    }
    
    //u3d自带特性，序列化该枚举对象时，单个选项前会有选择框，多个（比如ALL）会选中多个
    [System.Flags]
    public enum SomeBitmaskEnum
    {
        A=1<<1,
        B=1<<2,
        C=1<<3,
        
        Two=A|B,
        ALL=A|B|C
    }
}
