using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//如果ref指定的实参为null,则对形参的分配和修改都会同步到ref指定的实参引用。
//如果不指定ref，上述操作则不同步
public class TestRef : MonoBehaviour
{
    private GameObject _gameObject;
    // Start is called before the first frame update
    void Start()
    {
        GameObject goAsset = Resources.Load<GameObject>("HexagonlMap/Prefab/HexCellLabel");
        TestRefAssignment(ref _gameObject, goAsset);
        LogTool.Log("1111111_gameObject.name="+_gameObject.name);
        TestRefSetNull(ref _gameObject);
        LogTool.Log("22222_gameObject.name="+_gameObject.name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TestRefAssignment(ref GameObject go,GameObject goAsset)
    {
        go = GameObject.Instantiate(goAsset);
    }

    void TestRefSetNull(ref GameObject go)
    {
        go = null;
    }
}
