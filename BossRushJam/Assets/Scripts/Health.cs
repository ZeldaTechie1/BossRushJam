using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private float _maxHealth;
    [SerializeField]private GameEvent _deathEvent;
    [SerializeField]private float _health;
    [SerializeField]private bool _isVulnerable;

    private bool isDead;

    public void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(isDead || !_isVulnerable)
        {
            return;
        }
        _health -= damage;
        if(_health < 0)
        {
            _deathEvent.Invoke(this);
            isDead = true;
        }
    }

    public void Heal(float heal)
    {
        if(isDead)
        {
            return;
        }
        _health += heal;
        _health = Mathf.Clamp(_health,0,_maxHealth);
    }
    
    public void SetVulnerability(bool isVulnerable)
    {
        _isVulnerable = isVulnerable;
    }
}
