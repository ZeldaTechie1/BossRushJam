using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private float _maxHealth;
    [SerializeField]private GameEvent _deathEvent;
    [SerializeField]private float _health;
    [SerializeField]private bool _canTakeDamage;

    private bool isDead;

    public void Start()
    {
        _health = _maxHealth;
        _canTakeDamage = true;
    }

    public void AffectHealth(Component objectToDamage, object data)
    {
        if(isDead)
        {
            return;
        }
        if(data.GetType() != Type.GetType("System.Single"))
        {
            throw new System.Exception($"Wrong data when calling this function! Expecting float and received {data.GetType()}");
        }
        float healthDelta = (float)data;
        if((float)data < 0 && !_canTakeDamage)
        {
            return;
        }
        if(objectToDamage != null && objectToDamage.gameObject != this.gameObject)
        {
            return;
        }
        _health -= healthDelta;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        if (_health < 0)
        {
            _deathEvent.Invoke(this);
            isDead = true;
        }
    }
    
    public void SetVulnerability(Component sender, object data)
    {
        if(data.GetType() != Type.GetType("System.Boolean"))
        {
            throw new System.Exception($"Wrong data when calling this function! Expecting boolean and received {data.GetType()}");
        }
        _canTakeDamage = (bool)data;
    }
}
