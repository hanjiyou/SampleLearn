using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DelayedPropertyExample : MonoBehaviour
{
   [ShowInInspector]
   private int filed;
   [OnValueChanged("OnValueChanged")]
   [ShowInInspector]
   private int property { get; set; }

   void OnValueChanged()
   {
      LogTool.Log("OnvalueChanged回调");
   }
   
   [DelayedProperty]
   [ShowInInspector]
   [OnValueChanged("OnValueChanged2")]
   public int delayProperty { get; set; }

   void OnValueChanged2()
   {
      LogTool.Log("OnvalueChanged222回调");
   }
}
