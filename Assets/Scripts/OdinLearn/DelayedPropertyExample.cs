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
      Debug.Log("OnvalueChanged回调");
   }
   
   [DelayedProperty]
   [ShowInInspector]
   [OnValueChanged("OnValueChanged2")]
   public int delayProperty { get; set; }

   void OnValueChanged2()
   {
      Debug.Log("OnvalueChanged222回调");
   }
}
