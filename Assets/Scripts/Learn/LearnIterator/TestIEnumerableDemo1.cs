using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIEnumerableDemo1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var VARIABLE in TestEnumerable())
        {
            LogTool.Log(VARIABLE);
        }
    }

    IEnumerable<string> TestEnumerable()
    {
        yield return "start";
        for (int i = 0; i < 10; i++)
        {
            yield return i.ToString();
        }
        
        yield return "end";
    }
}
