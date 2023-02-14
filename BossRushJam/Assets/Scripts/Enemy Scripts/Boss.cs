using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Boss : MonoBehaviour
{
    [SerializeField]
    protected float _speed;

    protected Health _health;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected GameObject _player;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public abstract void Spawn();
    public abstract void Attack();
    public abstract void Move();
    public abstract void Die();
}