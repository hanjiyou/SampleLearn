using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonStart : MonoBehaviour
{
    private SingletonDemo1 _singletonDemo1;
    // Start is called before the first frame update
    void Start()
    {
        _singletonDemo1=SingletonCreator.CreateSingleton<SingletonDemo1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
