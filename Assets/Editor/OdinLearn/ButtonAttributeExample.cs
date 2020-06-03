using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ButtonAttributeExample : MonoBehaviour
{
    public string DynamicBtnName;
    [Button]
    private void Default()
    {
    }
    [Button]
    private void Default(float a, float b, GameObject c)
    {
    }

    [Button]
    private void Default(float t, float b, float[] c)
    {
    }
    
    [Button("$DynamicBtnName",ButtonSizes.Large),GUIColor(0.5f,0.5f,0.5f)]
    public void ExpressionLabel()
    {
        
    }
    [Title("ButtonStyle")]
    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    private int FoldoutButton(int a = 2, int b = 2)
    {
        return a + b;
    }

    [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
    private void FoldoutButton(int a, int b, ref int result)
    {
        result = a + b;
    }

    [Button(ButtonSizes.Large, ButtonStyle.Box)]
    private void Box(float a, float b, out float c)
    {
        c = a + b;
    }

    [Button(ButtonSizes.Large, ButtonStyle.Box)]
    private void Box(int a, float b, out float c)
    {
        c = a + b;
    }
    [Button(ButtonSizes.Large, ButtonStyle.CompactBox)]
    public void CompactBox(int a, float b, out float c)
    {
        c = a + b;
    }
}
