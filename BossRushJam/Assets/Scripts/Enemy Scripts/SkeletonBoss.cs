using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : Boss
{

    private float _stab2HAnimationLength = 0;
    private float _swipe2HAnimationLength = 0;
    private float _stab4HAnimationLength = 0;
    private float _swipe4HAnimationLength = 0;
    private Dictionary<string, float> _damageValues = new Dictionary<string, float>()
    {
        { "Stab2H", -5f},
        { "Swipe2H", -5f },
        { "Stab4H", -10f },
        { "Swipe4H", -10f },
        { "SwipeStab", -5f },
        { "SpinAttack", -10f }
    };

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            switch(clip.name)
            {
                case "Attack1_2Arms":
                    _swipe2HAnimationLength = clip.length;
                    break;
                case "Attack2_2Arms":
                    _stab2HAnimationLength= clip.length;
                    break;
                case "Attack3_4Arms":
                    _swipe4HAnimationLength= clip.length;
                    break;
                case "Attack4_4Arms":
                    _stab4HAnimationLength= clip.length;
                    break;
            }
        }
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
            SetAttack();
        }
        switch(_currentAttack)
        {
            case 0:
                Stab2H();
                break;
            case 1:
                Swipe2H();
                break;
            case 2:
                Stab4H();
                break;
            case 3:
                Swipe4H();
                break;
            case 4:
                SwipeStab();
                break;
            case 5:
                SpinAttack();
                break;
        }
        _agent.destination = _player.transform.position;
        
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        if (_currentPhase == 1)
        {

        }
        if (_currentPhase == _maxPhase)
        {
            IsMoving = false;
            _animator.SetTrigger("4ArmMode");
        }
    }

    private void Stab2H()
    {
        if(_currentPhase == _maxPhase)
        {
            Stab4H();
            return;
        }
        if (PlayerInAttackRange() && PlayerInFront())
        {
            _animator.SetBool("Stab2H", true);
            IsMoving = false;
            _agent.velocity = Vector3.zero;
            DOTween.Sequence().InsertCallback(_stab2HAnimationLength, () => _animator.SetBool("Stab2H", false));
        }
        else
        {
            IsMoving = true;
        }
    }

    private void Swipe2H()
    {
        if (PlayerInAttackRange() && PlayerInFront())
        {
            _animator.SetBool("Swipe2H", true);
            IsMoving = false;
            _agent.velocity = Vector3.zero;
            DOTween.Sequence().InsertCallback(_swipe2HAnimationLength, () => _animator.SetBool("Swipe2H", false));
            _currentAttack = -1;
        }
        else
        {
            IsMoving = true;
        }
    }

    private void Stab4H()
    {
        if (PlayerInAttackRange() && PlayerInFront())
        {
            _animator.SetBool("Stab4H", true);
            IsMoving = false;
            _agent.velocity = Vector3.zero;
            DOTween.Sequence().InsertCallback(_stab4HAnimationLength, () => _animator.SetBool("Stab4H", false));
            _currentAttack = -1;
        }
        else
        {
            IsMoving = true;
        }
    }

    private void Swipe4H()
    {
        if (PlayerInAttackRange() && PlayerInFront())
        {
            _animator.SetBool("Swipe4H", true);
            IsMoving = false;
            _agent.velocity = Vector3.zero;
            DOTween.Sequence().InsertCallback(_swipe4HAnimationLength, () => _animator.SetBool("Swipe4H", false));
            _currentAttack = -1;
        }
        else
        {
            IsMoving = true;
        }
    }

    private void SwipeStab()
    {
        if (PlayerInAttackRange() && PlayerInFront())
        {
            IsMoving = false;
            _agent.velocity = Vector3.zero;
            if (_currentPhase != _maxPhase)
            {
                _animator.SetBool("Stab2H", true);
                _animator.SetBool("Swipe2H", true);
            }
            else
            {
                _animator.SetBool("Stab4H", true);
                _animator.SetBool("Swipe4H", true);
            }
            DOTween.Sequence().InsertCallback(_swipe2HAnimationLength + _stab2HAnimationLength, () =>
            {
                _animator.SetBool("Stab2H", false);
                _animator.SetBool("Swipe2H", false);
                _animator.SetBool("Stab4H", false);
                _animator.SetBool("Swipe4H", false);
            });
            _currentAttack = -1;
        }
        else
        {
            IsMoving = true;
        }
    }
            
    private void SpinAttack()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health == null || !health.CanTakeDamage) { return; }
        health.AffectHealth(null, _damageValues[_attackAnimations[_currentAttack]]);
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        DOTween.Sequence().SetDelay(1).AppendCallback(() => { if (other == null) { return; } other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }
}
