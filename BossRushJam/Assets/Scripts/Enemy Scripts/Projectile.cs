using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected Vector3 _velocity;

    protected Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public abstract void Launch(Vector3 target);

    public abstract void HandleCollision(Collider other);

    public void OnTriggerEnter(Collider other)
    {
        HandleCollision(other);   
    }
}
