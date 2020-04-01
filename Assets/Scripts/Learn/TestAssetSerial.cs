using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class TestAssetSerial : MonoBehaviour
{
    public System.Collections.Generic.List<int> a;
    // Start is called before the first frame update
    void Start()
    {
        SystemConfig systemConfig= Resources.Load<SystemConfig>("GameData/NoUpdate/SystemConfig");
        systemConfig.InitList();
    }

    void InitList()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
