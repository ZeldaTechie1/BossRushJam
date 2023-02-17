using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampireBoss : Boss
{
    [SerializeField]
    private Transform[] _batWaypoints;
    [SerializeField]
    private ZombieBoss _zombieBoss;
    [SerializeField]                                                                
    private SkeletonBoss _skeletonBoss;
    [SerializeField]
    private SpriteRenderer _batSprite;
    [SerializeField]
    private Transform _beamWaypoint;
    [SerializeField]
    private MeshRenderer _beamRenderer;

    private Vector3 _originalBatPosition;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        _originalBatPosition = _batSprite.transform.localPosition;
    }

    private void OnEnable()
    {
        Spawn();
    }

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
            ResetAttackBools();
            SetAttack();
        }
        else
        {
            BeamAttack();
        }
    }

    private void ResetAttackBools()
    {
        foreach(string attack in _attackAnimations)
        {
            _animator.SetBool(attack, false);
        }
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        if (_currentPhase == 1)
        {

        }
        else if (_currentPhase == 2)
        {

        }
        else if (_currentPhase == _maxPhase)
        {

        }
    }

    private void BatTransformation()
    {
        //TODO Play poof particle
        IsMoving = false;
        Transform(true, true);
        List<Transform> waypoints = _batWaypoints.ToList();
        Sequence sequence = DOTween.Sequence();
        for(int i = 0; i < _batWaypoints.Length; i++)
        {
            Transform waypoint = waypoints[Random.Range(0, waypoints.Count)];
            
            sequence.AppendCallback(() => 
            {
                Vector3 direction = (_batSprite.transform.position - waypoint.position).normalized;
                if (direction.x > 0)
                {
                    _batSprite.flipX = true;
                }
                else
                {
                    _batSprite.flipX = false;
                }
                _batSprite.transform.DOMove(waypoint.position, 1f); 
            }).AppendInterval(1f);
            waypoints.Remove(waypoint);
        }
        sequence.Append(_batSprite.transform.DOLocalMove(_originalBatPosition, 1f)).AppendCallback(() =>
        {
            Transform(false, false);
        }).AppendInterval(2f).AppendCallback(() =>
        {
            IsMoving = true;
        });
    }

    private void Swipe()
    {
        if (PlayerInAttackRange() && PlayerInFront())
        {
            IsMoving = false;
            _animator.SetBool(_attackAnimations[_currentAttack], true);
            if (_currentPhase >= 1)
            {
                _currentAttack++;
                _animator.SetBool(_attackAnimations[_currentAttack], true);
            }
            _currentAttack = -1;
            _agent.velocity = Vector3.zero;
            return;
        }
        _agent.destination = _player.transform.position;
    }

    private void BeamAttack()
    {
        if (_currentTarget == null)
        {
            _currentTarget = _beamWaypoint;
            _agent.SetDestination(_currentTarget.position);
        }
        if (_agent.isStopped || _agent.isPathStale || Vector3.Distance(transform.position, _currentTarget.position) <= _waypointThreshold)
        {
            IsMoving = false;
            _agent.ResetPath();
            Beam();
        }
    }

    public void Beam()
    {
        IsMoving = false;
        Transform(true);
        _beamRenderer.GetComponent<Collider>().enabled = true;
        Vector3 originalPos = _batSprite.transform.localPosition;
        _batSprite.transform.localPosition = new Vector3(originalPos.x, 2, originalPos.z);
        DOTween.Sequence().Append(_beamRenderer.transform.DOScaleZ(0.25f, 0.5f).SetEase(Ease.InSine)).
            Append(transform.DOLocalMoveZ(10, 3f)).Append(transform.DOLocalMoveZ(-10, 3f)).Append(_beamRenderer.transform.DOScaleZ(0, 0.5f).SetEase(Ease.OutSine)).
            AppendCallback(() =>
            {
                transform.DOMove(_currentTarget.transform.position, 1f).OnComplete(() =>
                {
                    _currentTarget = null;
                    IsMoving = true;
                    Transform(false);
                    _batSprite.transform.localPosition = originalPos;
                    _currentAttack = -1;
                });
                _beamRenderer.GetComponent<Collider>().enabled = false;
            });
    }

    private void Transform(bool bat, bool enableBatCollider = false)
    {
        _batSprite.enabled = bat;
        _batSprite.GetComponent<Collider>().enabled = enableBatCollider;
        GetComponent<Collider>().enabled = !bat;
        _animator.speed = 0;
        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            smr.enabled = !bat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health == null || !health.CanTakeDamage) { return; }
        health.AffectHealth(null, -10f);
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        DOTween.Sequence().SetDelay(1).AppendCallback(() => { if (other == null) { return; } other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }
    private void SummonZombie()
    {

    }

    private void SummonSkeleton()
    {

    }
}
