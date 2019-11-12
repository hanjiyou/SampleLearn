using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 协程执行完毕，协程对象也不会为空
/// </summary>
public class TestCoroutine : MonoBehaviour
{
    private Coroutine _coroutine;
    // Start is called before the first frame update
    void Start()
    {
        _coroutine=StartCoroutine(TestCor1());
    }

    // Update is called once per frame
    void Update()
    {
        if (_coroutine == null)
        {
            LogTool.Log("hhh _coroutine为空");
        }
        else
        {
            LogTool.Log("hhh _coroutine不");
        }
    }

    IEnumerator TestCor1()
    {
        LogTool.Log("hhh TestCor1 111");
        yield return new WaitForSeconds(1);
        LogTool.Log("hhh TestCor1 2");
    }
}
