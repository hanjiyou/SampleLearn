using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnableGUIExample : MonoBehaviour
{
     [ShowInInspector]
     public int DiableProperty
     {
          get { return 12; }
     }
     
     [ShowInInspector,EnableGUI]
     public int EnableProperty
     {
          get { return 12; }
     }
}
