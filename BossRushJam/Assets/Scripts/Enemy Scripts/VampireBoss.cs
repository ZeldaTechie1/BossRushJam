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

    private Vector3 _originalBatPosition;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        _originalBatPosition = _batSprite.transform.localPosition;
        IsMoving = true;
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
            //SetAttack();
        }
        BatTransformation();
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
        _batSprite.enabled = true;
        _batSprite.GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().enabled = false;
        _animator.speed = 0;
        foreach(SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            smr.enabled = false;
        }
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
                    _batSprite.flipY = true;
                }
                else
                {
                    _batSprite.flipY = false;
                }
                _batSprite.transform.DOMove(waypoint.position, 1f); 
            }).AppendInterval(1f);
            waypoints.Remove(waypoint);
        }
        sequence.Append(_batSprite.transform.DOLocalMove(_originalBatPosition, 1f)).AppendCallback(() =>
        {
            _batSprite.enabled = false;
            _batSprite.GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().enabled = true;
            _animator.speed = 1;
            foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                smr.enabled = true;
            }
        }).AppendInterval(2f).AppendCallback(() =>
        {
            IsMoving = true;
        });
    }

    private void Swipe()
    {

    }

    private void Backhand()
    {

    }

    private void BeamAttack()
    {

    }

    private void SummonZombie()
    {

    }

    private void SummonSkeleton()
    {

    }
}
