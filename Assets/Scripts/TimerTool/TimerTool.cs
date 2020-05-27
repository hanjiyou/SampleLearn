using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer
{
    protected float _delayTimer;
    protected float _delayTime;
    protected bool _isOver;
    protected bool _isPause;

    protected Timer(float delayTime)
    {
        _delayTime = delayTime;
    }
    public bool OnUpdate()//这个在Mono的LateUpdate()中调用
    {
        if (_isOver)
        {
            return false;
        }

        if (_isOver)
        {
            return false;
        }

        _delayTimer += Time.deltaTime;
        if (_delayTimer >= _delayTime)
            return Update();
        return false;
    }

    public void SetPause()
    {
        _isPause = true;
    }

    public void Recover()
    {
        _isPause = false;
    }
    public bool GetOver()
    {
        return _isOver;
    }
    public void Release()
    {
        _isOver = false;
        _isPause = false;
        _delayTime = 0;
        _delayTimer = 0;
    }
    protected abstract bool Update();
}

public class OnceTimer : Timer
{
    public OnceTimer(float delayTime) : base(delayTime)
    {
        
    }
    protected override bool Update()
    {
        return true;
    }
}


public class RepeatTimer : Timer
{
    private float _delayTime;
    private int _repeatTime;
    private int _repeatTimer;
    public RepeatTimer(float delayTime,int repeatTime) : base(delayTime)
    {
        _delayTime = delayTime;
        _repeatTime = repeatTime;
    }
    
    protected override bool Update()
    {
        if (_repeatTimer < _repeatTime)
        {
            _delayTimer += Time.deltaTime;
            if (_delayTimer >= _delayTime)
            {
                _delayTimer = 0;
                _repeatTimer++;
                return true;
            }
            return false;
        }

        _isOver = true;
        return false;
    }
}

