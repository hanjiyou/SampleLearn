using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PreviewFieldAttributeExample : MonoBehaviour
{
    [VerticalGroup("MyGroup/ver")]
    public string A, B, C;

    [HideLabel]
    [PreviewField(ObjectFieldAlignment.Right)]
    [HorizontalGroup("MyGroup"),VerticalGroup("MyGroup/ver2")]
    public GameObject Right;
    
    
}
