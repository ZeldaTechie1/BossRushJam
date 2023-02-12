using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState { MOVING, CHASING, ATTACKING}

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
    private Transform[] _shockwaveWaypointsLeft;
    [SerializeField]
    private Transform[] _shockwaveWaypointsRight;
    [SerializeField]
    private Transform[] _throwWaypoints;
    [SerializeField]
    private float _waypointThreshold = 0.5f;

    public bool IsMoving { get { return _isMoving; } set { _isMoving = value; agent.isStopped = !value; _animator.SetBool("Walk", value); } }
    public bool IsAttacking;
    private int _attackRange = 5;
    private bool _isMoving;
    private NavMeshAgent agent;
    private Transform _currentTarget;
    private Transform _lookAtTarget;

    private int _throwCount = 0;

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
        // Throw
        if (_currentTarget == null)
        {
            _currentTarget = _throwWaypoints[Random.Range(0, _throwWaypoints.Length)];
            agent.SetDestination(_currentTarget.position);
        }
        if (agent.isStopped || agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        {
            IsMoving = false;
            _currentTarget = null;
            _animator.SetBool(attackAnimations[2], true);
        }
        // Shockwave
        //if (_currentTarget == null && IsMoving)
        //{
        //    bool left = Random.Range(0, 2) == 0;
        //    int index = Random.Range(0, _shockwaveWaypointsLeft.Length);
        //    if (left)
        //    {
        //        _currentTarget = _shockwaveWaypointsLeft[index];
        //        _lookAtTarget = _shockwaveWaypointsRight[index];
        //    }
        //    else
        //    {
        //        _currentTarget = _shockwaveWaypointsRight[index];
        //        _lookAtTarget = _shockwaveWaypointsLeft[index];
        //    }
        //    agent.SetDestination(_currentTarget.position);
        //}
        //if (agent.isStopped || agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        //{
        //    IsMoving = false;
        //    _currentTarget = null;
        //    agent.ResetPath();
        //    transform.DOLookAt(_lookAtTarget.position, 0.5f).OnComplete(() =>
        //    {
        //        _animator.SetTrigger(attackAnimations[1]);
        //    });
        //}
        // Chase
        //Vector3 directionToTarget = _player.transform.position - transform.position;
        //if (directionToTarget.magnitude < _attackRange)
        //{

        //    if (Mathf.Acos(Vector3.Dot(directionToTarget.normalized, transform.forward)) * Mathf.Rad2Deg < 25)
        //    {
        //        _animator.SetTrigger(attackAnimations[0]);
        //        IsMoving = false;
        //        agent.velocity = Vector3.zero;
        //        return;
        //    }
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
        wave.Launch(_lookAtTarget.gameObject);
    }

    public void ThrowAttack()
    {
        _throwCount++;
        if (_throwCount == 3)
        {
            _throwCount = 0;
            _animator.SetBool(attackAnimations[2], false);
        }
        Tombstone tombstone = Instantiate(_tombstonePrefab);
        tombstone.transform.position = _throwStartLocation.position;
        tombstone.Launch(_player);
    }
}
