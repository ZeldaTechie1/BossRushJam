using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    public GameEvent StartWaveEvent;
    public GameEvent EnemyDiedEvent;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartWaveEvent.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            EnemyDiedEvent.Invoke();
        }
    }
}
