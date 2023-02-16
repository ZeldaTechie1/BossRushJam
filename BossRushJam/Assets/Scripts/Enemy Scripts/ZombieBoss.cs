using DG.Tweening;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CheckForPhaseChange(true);
        }
        if (!IsMoving || _health.IsDead)
        {
            return;
        }
        if (_currentAttack < 0)
        {
            SetAttack();
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

    public override void ChangePhase()
    {
        base.ChangePhase();
        if (_currentPhase == 1)
        {
            _animator.SetFloat("Speed", 1.25f);
            _agent.speed = 5;
            _agent.angularSpeed = 180;
        }
        if (_currentPhase == _maxPhase)
        {
            _animator.SetFloat("Speed", 1.5f);
            _agent.speed = 10;
            _agent.angularSpeed = 360;
            _animator.SetBool("CanTeleport", true);
        }
    }

    public override void Teleport()
    {
        base.Teleport();
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

    private void Bite()
    {
        if(PlayerInAttackRange() && PlayerInFront())
        { 
            _animator.SetTrigger(_attackAnimations[_currentAttack]);
            IsMoving = false;
            _currentAttack = -1;
            _agent.velocity = Vector3.zero;
            _gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayZombieBite();
            return;
        }
        _agent.destination = _player.transform.position;
    }

    private void Shockwave()
    {
        if (_currentPhase == _maxPhase)
        {
            IsMoving = false;
            _animator.SetBool(_attackAnimations[_currentAttack], true);
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
            _agent.SetDestination(_currentTarget.position);
        }
        if (_agent.isStopped || _agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        {
            IsMoving = false;
            _currentTarget = null;
            _agent.ResetPath();
            transform.DOLookAt(_lookAtTarget.position, 0.5f).OnComplete(() =>
            {
                _animator.SetBool(_attackAnimations[_currentAttack], true);
            });
        }
    }

    private void Throw()
    {
        if (_currentPhase == _maxPhase)
        {
            IsMoving = false;
            _animator.SetBool(_attackAnimations[_currentAttack], true);
            return;
        }
        if (_currentTarget == null)
        {
            _currentTarget = _throwWaypoints[Random.Range(0, _throwWaypoints.Length)];
            _agent.SetDestination(_currentTarget.position);
        }
        if (_agent.isStopped || _agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        {
            IsMoving = false;
            _currentTarget = null;
            _animator.SetBool(_attackAnimations[_currentAttack], true);
        }
    }

    public void ShockwaveAttack()
    {
        _gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayZombieShockwave();
        _attackCount++;
        if (_attackCount == _currentPhase + 1)
        {
            _attackCount = 0;
            _animator.SetBool(_attackAnimations[1], false);
            _currentAttack = -1;
            if (_currentPhase == _maxPhase)
            {
                _animator.SetBool(_attackAnimations[2], true);
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
        _gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayZombieTomstoneToss();
        _attackCount++;
        if (_attackCount == _currentPhase + 1)
        {
            _attackCount = 0;
            _animator.SetBool(_attackAnimations[2], false);
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
