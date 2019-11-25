using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventExecute : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventPublisher gameEventPublisher=new GameEventPublisher();
        GameEventSubscribe gameEventSubscribe1=new GameEventSubscribe("sub1",gameEventPublisher);
        GameEventSubscribe gameEventSubscribe2=new GameEventSubscribe("sub2",gameEventPublisher);
        gameEventPublisher.DoSomething();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
