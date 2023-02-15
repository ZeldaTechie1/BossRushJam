using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public bool CanTakeDamage;
    [HideInInspector]
    public bool IsStunned;
    [HideInInspector]
    public bool IsDead;

    [SerializeField]private float _maxHealth;
    [SerializeField]private GameEvent _deathEvent;
    [SerializeField]private float _health;
    [SerializeField]private ParticleSystem _bloodEffect;

    public Action HealthAffected;

    public void Start()
    {
        _health = _maxHealth;
        CanTakeDamage = true;
    }

    public float GetHealthPercentage()
    {
        return _health / _maxHealth;
    }

    public void AffectHealth(Component objectToDamage, object data)
    {
        if(IsDead)
        {
            return;
        }
        if(data.GetType() != Type.GetType("System.Single"))
        {
            throw new System.Exception($"Wrong data when calling this function! Expecting float and received {data.GetType()}");
        }
        
        float healthDelta = (float)data;
        if (!CanTakeDamage)
        {
            return;
        }
        if(objectToDamage != null && objectToDamage.gameObject != this.gameObject)
        {
            return;
        }
        _health += healthDelta;
        _health = Mathf.Clamp(_health, -1000, _maxHealth);
        if(healthDelta < 0 && _bloodEffect != null)
        {
            _bloodEffect.Stop();
            _bloodEffect.Play();
        }
        if (_health <= 0)
        {
            _deathEvent.Invoke(this);
            IsDead = true;
        }
        else
        {
            HealthAffected?.Invoke();
        }
    }
    
    public void SetVulnerability(Component sender, object data)
    {
        if(data.GetType() != Type.GetType("System.Boolean"))
        {
            throw new System.Exception($"Wrong data when calling this function! Expecting boolean and received {data.GetType()}");
        }
        CanTakeDamage = (bool)data;
    }
}
