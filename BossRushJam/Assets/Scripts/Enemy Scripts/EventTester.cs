using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTester : MonoBehaviour
{
    public GameEvent StartWaveEvent;
    public GameEvent EnemyDiedEvent;

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.End))
        {
            StartWaveEvent.Invoke();
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Player Died");
        Destroy(this.gameObject);
    }
}
