using DG.Tweening;
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

    [HideInInspector]
    public bool IsInvincible;

    [SerializeField]private float _maxHealth;
    [SerializeField]private GameEvent _deathEvent;
    [SerializeField]private float _health;
    [SerializeField]private ParticleSystem _bloodEffect;
    [SerializeField]private ParticleSystem _poof;

    [SerializeField]protected GameAudioEventManager _gameAudioEventManager;

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
        if(IsDead || IsInvincible)
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

        if (this.name == "Player")
        {
            _gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayPlayerDamaged();
        } else {
            string sfx_enemy_damaged_path = "event:/SFX/sfx_enemy_damaged";
            FMOD.Studio.EventInstance sfx_enemy_damaged = FMODUnity.RuntimeManager.CreateInstance(sfx_enemy_damaged_path);
            sfx_enemy_damaged.start();
        }

        HealthAffected?.Invoke();

        if (_health <= 0)
        {
            if(_poof != null)
            {
                GameObject poof = Instantiate(_poof.gameObject, _poof.transform.position, Quaternion.identity);
                poof.transform.parent = null;
                poof.GetComponent<ParticleSystem>().Play();
            }

            _deathEvent.Invoke(this);
            IsDead = true;
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
