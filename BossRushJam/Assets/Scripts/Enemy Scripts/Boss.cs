using DG.Tweening;
using DG.Tweening.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(GameEventListener))]
public abstract class Boss : MonoBehaviour
{
    [SerializeField]
    protected int _bossIndex;
    [SerializeField]
    protected float[] _phaseThresholds;
    [SerializeField]
    protected float _waypointThreshold;
    [SerializeField]
    protected int _attackRange = 5;
    [SerializeField]
    protected string[] _attackAnimations;
    [SerializeField]
    protected List<float> _phaseOneAttackWeights = new List<float>();
    [SerializeField]
    protected List<float> _phaseTwoAttackWeights = new List<float>();
    [SerializeField]
    protected List<float> _phaseThreeAttackWeights = new List<float>();
    [SerializeField]
    protected Boss _nextBoss = null;
    [SerializeField]
    protected List<Enemy> _minionsToSpawn = new List<Enemy>();

    public bool IsMoving { get { return _isMoving; } set { _isMoving = value; _agent.isStopped = !value; _animator.SetBool("Walk", value); } }
    [HideInInspector]
    public bool IsAttacking;
    public Boss NextBoss { get { return _nextBoss; } }
    public int BossIndex { get { return _bossIndex; } }
    public List<Enemy> MinionsToSpawn { get { return _minionsToSpawn; } }

    public static Action<Boss> BossSpawned;
    public Action SpawnNextBoss;

    protected Health _health;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected GameObject _player;
    protected NavMeshAgent _agent;
    protected bool _isMoving;
    protected Transform _currentTarget;
    protected Transform _lookAtTarget;
    protected bool _isTeleporting = false;
    protected int _currentAttack = -1;
    protected float _nextPhaseThreshold;
    protected int _currentPhase = 0;
    protected int _maxPhase;
    protected List<List<float>> _attackWeightsPerPhase = new List<List<float>>();
    protected int _attackCount = 0;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _agent = GetComponent<NavMeshAgent>();
        _health.HealthAffected += CheckForPhaseChange;
        _maxPhase = _phaseThresholds.Length;
        _nextPhaseThreshold = _phaseThresholds[0];

        if (_phaseOneAttackWeights.Count > 0)
        {
            _attackWeightsPerPhase.Add(_phaseOneAttackWeights);
        }
        if (_phaseTwoAttackWeights.Count > 0)
        {
            _attackWeightsPerPhase.Add(_phaseTwoAttackWeights);
        }
        if (_phaseThreeAttackWeights.Count > 0)
        {
            _attackWeightsPerPhase.Add(_phaseThreeAttackWeights);
        }

        if (_attackWeightsPerPhase.Count != _phaseThresholds.Length + 1)
        {
            //Debug.LogError("Attack Weights Per Phase count didn't match the amount of phases given");
        }
        foreach (List<float> weights in _attackWeightsPerPhase)
        {
            if (weights.Count != _attackAnimations.Length)
            {
                //Debug.LogError("The amount of weights per phase doesn't match the amount of attack animations");
            }
        }
    }

    public virtual void Spawn()
    {
        BossSpawned?.Invoke(this);
        EnemyWaveController.Instance.StartNextPhase();
    }
    public virtual void SetAttack()
    {
        float randomNum = UnityEngine.Random.Range(0, _attackWeightsPerPhase[_currentPhase].Sum());
        float runningTotal = 0;
        for(int i = 0; i < _attackWeightsPerPhase[_currentPhase].Count; i++)
        {
            if (randomNum < _attackWeightsPerPhase[_currentPhase][i] + runningTotal)
            {
                _currentAttack = i;
                return;
            }
            runningTotal += _attackWeightsPerPhase[_currentPhase][i];
        }
    }

    public virtual void Die()
    {
        _animator.Play("Death");
        IsMoving = false;
        _agent.ResetPath();
        _currentAttack = -1;
        _currentTarget = null;
        DOTween.Sequence().SetDelay(2f).AppendCallback(() => 
        {
            transform.DOLocalMoveY(-transform.up.y * 2, 5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }).AppendInterval(2.5f).AppendCallback(() =>
        {
            SpawnNextBoss?.Invoke();
            if (_nextBoss)
            {
                _nextBoss.transform.position = transform.position;
                _nextBoss.gameObject.SetActive(true);
            }
        });
    }

    public virtual void Teleport()
    {
        if (_isTeleporting)
        {
            _currentTarget = null;
            _isTeleporting = false;
            return;
        }
    }

    public virtual void ChangePhase()
    {
        _currentPhase++;
        EnemyWaveController.Instance.StartNextPhase();
        if (IsMoving)
        {
            _currentAttack = -1;
            IsMoving = true;
            _currentTarget = null;
            _lookAtTarget = null;
            _agent.ResetPath();
        }
    }

    protected void CheckForPhaseChange(bool force = false)
    {
        if (_currentPhase == _maxPhase)
        {
            return;
        }
        if (_health.GetHealthPercentage() <= _nextPhaseThreshold || force)
        {
            ChangePhase();
            if (_currentPhase != _maxPhase)
            {
                _nextPhaseThreshold = _phaseThresholds[_currentPhase];
            }
        }
    }

    protected bool PlayerInAttackRange()
    {
        Vector3 directionToTarget = _player.transform.position - transform.position;
        if (directionToTarget.magnitude < _attackRange)
        {
            return true;
        }
        return false;
    }

    protected bool PlayerInFront()
    {
        Vector3 directionToTarget = _player.transform.position - transform.position;
        if (Mathf.Acos(Vector3.Dot(directionToTarget.normalized, transform.forward)) * Mathf.Rad2Deg < 25)
        {
            return true;
        }
        return false;
    }

    protected void CheckForPhaseChange()
    {
        CheckForPhaseChange(false);
    }
}