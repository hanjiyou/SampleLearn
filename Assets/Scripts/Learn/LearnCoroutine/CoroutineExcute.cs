using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExcute : MonoBehaviour
{
    private IEnumerator _enumerator;

    private OnceTimer _onceTimer;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    IEnumerator MyCor()
    {
        LogTool.Log($"hhh{Time.frameCount} 111");
        yield return null;
        LogTool.Log($"hhh{Time.frameCount} 222");
        yield return new WaitForSeconds(3);
        LogTool.Log($"hhh{Time.frameCount} 333");
        yield return null;
        LogTool.Log($"hhh{Time.frameCount} 444");
    }

    private void LateUpdate()
    {
        if (_enumerator != null)
        {
            bool needWait = _enumerator.Current is WaitForSeconds;
            LogTool.Log("hhh moveNext之前");
            if (needWait)
            {
                if (_onceTimer == null)
                {
                    _onceTimer=new OnceTimer(3);
                }
                else
                {
                    _onceTimer.OnUpdate();
                     
                }
            }

            if (needWait && !_onceTimer.OnUpdate())
            {
                return;
            }
            if (!_enumerator.MoveNext())
            {
                _enumerator = null;
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("btnaaaaaaaaaaaaaaaaaaaaa"))
        {
            _enumerator = MyCor();
        }
    }
}
