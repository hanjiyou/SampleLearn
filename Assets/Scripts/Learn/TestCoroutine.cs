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
            Loger.Log("hhh _coroutine为空");
        }
        else
        {
            Loger.Log("hhh _coroutine不");
        }
    }

    IEnumerator TestCor1()
    {
        Loger.Log("hhh TestCor1 111");
        yield return new WaitForSeconds(1);
        Loger.Log("hhh TestCor1 2");
    }
}
