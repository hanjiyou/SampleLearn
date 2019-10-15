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
      Loger.Log("OnvalueChanged回调");
   }
   
   [DelayedProperty]
   [ShowInInspector]
   [OnValueChanged("OnValueChanged2")]
   public int delayProperty { get; set; }

   void OnValueChanged2()
   {
      Loger.Log("OnvalueChanged222回调");
   }
}
