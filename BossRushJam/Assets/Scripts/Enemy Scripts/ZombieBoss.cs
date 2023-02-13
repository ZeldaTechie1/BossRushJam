using DG.Tweening;
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
    private bool _isTeleporting = false;
    private int _throwCount = 0;
    private float _nextPhaseThreshold = 0.75f;
    private int _currentPhase = 0;
    private string[] attackAnimations = {"Bite", "Slam", "Throw"};
    private float[] _phaseThresholds = { 0.66f, 0.33f };
    private int _maxPhase;
    private int _currentAttack = -1;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _health.HealthAffected += CheckForPhaseChange;
        _maxPhase = _phaseThresholds.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CheckForPhaseChange(true);
        }
        if (!IsMoving)
        {
            return;
        }
        if (_currentAttack < 0)
        {
            DetermineAttack();
        }
        if (_currentAttack == 0)
        {
            Bite();
        }
        else if (_currentAttack == 1)
        {
            Shockwave();
        }
        else if (_currentAttack == 2)
        {
            Throw();
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        _animator.Play("Death");
        IsMoving = false;
        _currentAttack = -1;
        _currentTarget = null;
    }

    public override void Move()
    {

    }

    private void DetermineAttack()
    {
        int[] weights;
        if (_currentPhase == 0)
        {
            weights = new int[3] { 60, 80, 100 };
        }
        else if (_currentPhase == 1)
        {
            weights = new int[3] { 30, 80, 100 };
        }
        else
        {
            weights = new int[3] { 20, 60, 100 };
        }
        int randomNum = Random.Range(0, 101);
        if (randomNum < weights[0])
        {
            _currentAttack = 0;
        }
        else if (randomNum < weights[1]) 
        {
            _currentAttack = 1;
        }
        else if (randomNum < weights[2]) 
        {
            _currentAttack = 2;
        }
    }

    private void ChangePhase()
    {
        if (IsMoving)
        {
            _currentAttack = -1;
            IsMoving = true;
            _currentTarget = null;
            _lookAtTarget = null;
            agent.ResetPath();
        }
        Color color = Color.white;
        if (_currentPhase == 1)
        {
            color = Color.yellow;
            _animator.SetFloat("Speed", 1.25f);
            agent.speed = 5;
            agent.angularSpeed = 180;
        }
        if (_currentPhase == _maxPhase)
        {
            color = Color.red;
            _animator.SetFloat("Speed", 1.5f);
            agent.speed = 10;
            agent.angularSpeed = 360;
            _animator.SetBool("CanTeleport", true);
        }
        foreach(SkinnedMeshRenderer mr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mr.material.color = color;
        }
    }

    private void CheckForPhaseChange(bool force = false)
    {
        if (_currentPhase == _maxPhase)
        {
            return;
        }
        if (_health.GetHealthPercentage() <= _nextPhaseThreshold || force)
        {
            EnemyWaveController.Instance.StartNextPhase();
            _currentPhase++;
            ChangePhase();
            if (_currentPhase != _maxPhase)
            {
                _nextPhaseThreshold = _phaseThresholds[_currentPhase];
            }
        }
    }

    private void CheckForPhaseChange()
    {
        CheckForPhaseChange(false);
    }

    public void Teleport()
    {
        if (_isTeleporting)
        {
            _currentTarget = null;
            _isTeleporting = false;
            return;
        }
        if (_currentTarget == null)
        {
            _isTeleporting = true;
            if (_currentAttack == 1)
            {
                bool left = Random.Range(0, 2) == 0;
                int index = Random.Range(0, _shockwaveWaypointsLeft.Length);
                if (left)
                {
                    _currentTarget = _shockwaveWaypointsLeft[index];
                    _lookAtTarget = _shockwaveWaypointsRight[index];
                }
                else
                {
                    _currentTarget = _shockwaveWaypointsRight[index];
                    _lookAtTarget = _shockwaveWaypointsLeft[index];
                }
                transform.position = _currentTarget.position;
                transform.LookAt(_lookAtTarget.position);
            }
            else
            {
                _currentTarget = _throwWaypoints[Random.Range(0, _throwWaypoints.Length)];
                transform.position = _currentTarget.position;
            }
        }
    }

    public override void Spawn()
    {
        EnemyWaveController.Instance.StartNextPhase();
    }

    private void Bite()
    {
        Vector3 directionToTarget = _player.transform.position - transform.position;
        if (directionToTarget.magnitude < _attackRange)
        {
            if (Mathf.Acos(Vector3.Dot(directionToTarget.normalized, transform.forward)) * Mathf.Rad2Deg < 25)
            {
                _animator.SetTrigger(attackAnimations[_currentAttack]);
                IsMoving = false;
                _currentAttack = -1;
                agent.velocity = Vector3.zero;
                return;
            }
        }
        agent.destination = _player.transform.position;
    }

    private void Shockwave()
    {
        if (_currentPhase == _maxPhase)
        {
            IsMoving = false;
            _animator.SetBool(attackAnimations[_currentAttack], true);
            return;
        }
        if (_currentTarget == null)
        {
            bool left = Random.Range(0, 2) == 0;
            int index = Random.Range(0, _shockwaveWaypointsLeft.Length);
            if (left)
            {
                _currentTarget = _shockwaveWaypointsLeft[index];
                _lookAtTarget = _shockwaveWaypointsRight[index];
            }
            else
            {
                _currentTarget = _shockwaveWaypointsRight[index];
                _lookAtTarget = _shockwaveWaypointsLeft[index];
            }
            agent.SetDestination(_currentTarget.position);
        }
        if (agent.isStopped || agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        {
            IsMoving = false;
            _currentTarget = null;
            agent.ResetPath();
            transform.DOLookAt(_lookAtTarget.position, 0.5f).OnComplete(() =>
            {
                _animator.SetBool(attackAnimations[_currentAttack], true);
            });
        }
    }

    private void Throw()
    {
        if (_currentPhase == _maxPhase)
        {
            IsMoving = false;
            _animator.SetBool(attackAnimations[_currentAttack], true);
            return;
        }
        if (_currentTarget == null)
        {
            _currentTarget = _throwWaypoints[Random.Range(0, _throwWaypoints.Length)];
            agent.SetDestination(_currentTarget.position);
        }
        if (agent.isStopped || agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        {
            IsMoving = false;
            _currentTarget = null;
            _animator.SetBool(attackAnimations[_currentAttack], true);
        }
    }

    public void ShockwaveAttack()
    {
        _throwCount++;
        if (_throwCount == _currentPhase + 1)
        {
            _throwCount = 0;
            _animator.SetBool(attackAnimations[1], false);
            _currentAttack = -1;
            if (_currentPhase == _maxPhase)
            {
                _animator.SetBool(attackAnimations[2], true);
                _currentAttack = 2;
            }
        }
        Shockwave wave = Instantiate(_shockwavePrefab);
        wave.transform.rotation = _shockwaveStartLocation.rotation;
        wave.transform.position = _shockwaveStartLocation.position;
        wave.Launch(_lookAtTarget.gameObject);
    }

    public void ThrowAttack()
    {
        _throwCount++;
        if (_throwCount == _currentPhase + 1)
        {
            _throwCount = 0;
            _animator.SetBool(attackAnimations[2], false);
            _currentAttack = -1;
        }
        Tombstone tombstone = Instantiate(_tombstonePrefab);
        tombstone.transform.position = _throwStartLocation.position;
        tombstone.Launch(_player);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health == null || !health.CanTakeDamage) { return; }
        health.AffectHealth(null, -10f);
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        DOTween.Sequence().SetDelay(1).AppendCallback(() => { if (other == null) { return; } other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }
}
