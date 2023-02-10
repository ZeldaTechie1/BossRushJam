using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBoss : Boss
{
    [SerializeField]
    private Shockwave _shockwavePrefab;
    [SerializeField]
    private Tombstone _tombstonePrefab;
    [SerializeField]
    private Transform _shockwaveStartLocation;
    [SerializeField]
    private Transform _throwStartLocation;
    [SerializeField]
    private Transform[] _shockwaveWaypoints;
    [SerializeField]
    private Transform[] _throwWaypoints;
    [SerializeField]
    private float _waypointThreshold = 0.5f;

    public bool IsMoving { get { return _isMoving; } set { _isMoving = value; agent.isStopped = !value; _animator.SetBool("Walk", value); } }
    public bool IsAttacking;
    private int _attackRange = 5;
    private bool _isMoving;
    private NavMeshAgent agent;
    private Transform _currentWaypoint;

    private string[] attackAnimations = {"Bite", "Slam", "Throw"};
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            Move();
        }
        else if(IsAttacking)
        {
            _animator.SetTrigger(attackAnimations[Random.Range(0, 3)]);
            IsAttacking = false;
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        if (_currentWaypoint == null)
        {
            _currentWaypoint = _shockwaveWaypoints[Random.Range(0, _shockwaveWaypoints.Length)];
            _currentWaypoint.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            agent.SetDestination(_currentWaypoint.position);
        }
        if (agent.isStopped || agent.isPathStale || Vector3.Distance(transform.position, _currentWaypoint.position) <= _waypointThreshold)
        {
            _currentWaypoint.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            _currentWaypoint = null;
        }
        //if (Vector3.Distance(transform.position, _player.transform.position) < _attackRange)
        //{
        //    IsMoving = false;
        //    agent.velocity = Vector3.zero;
        //    return;
        //}
        //agent.destination = _player.transform.position;
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }

    private void BiteAttack()
    {

    }

    public void ShockwaveAttack()
    {
        Shockwave wave = Instantiate(_shockwavePrefab);
        wave.transform.rotation = _shockwaveStartLocation.rotation;
        wave.transform.position = _shockwaveStartLocation.position;
        wave.Launch(_shockwaveStartLocation.position + transform.forward * 50);
    }

    public void ThrowAttack()
    {
        Tombstone tombstone = Instantiate(_tombstonePrefab);
        tombstone.transform.position = _throwStartLocation.position;
        tombstone.Launch(_player.transform.position);
    }
}
