using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private Camera _mainCamera;
    [SerializeField]private float _movementSpeed;
    [SerializeField]private float _slideLength;
    [SerializeField]private float _slideSpeed;
    [SerializeField]private float _slideCoolDown;
    [SerializeField]private float _invincibilityLength;
    [SerializeField]private GameEvent _playerCanTakeDamageEvent;
    [SerializeField]private List<Animator> _playerAnimators;
    [SerializeField]private List<GameObject> _hitboxes;
    [SerializeField]private float _baseDamage = 35;
    [SerializeField]private float _attackCoolDown;

    private Rigidbody _rb;
    private Vector2 _movementInput;
    private Vector2 _lookDirection;
    private Vector3 _movementDirection;
    private Vector3 _cardinalMoveDirection;
    private Vector3 currentOrientation;
    private int _lookDirectionIndex;
    private bool _isSliding;
    private bool _isAttacking;
    private bool _canSlide = true;
    private bool _canAttack = true;
    private float _attackTime = 0;
    private Health health;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        foreach (AnimationClip clip in _playerAnimators[0].runtimeAnimatorController.animationClips)
        {
            if(clip.name.Contains("Attack"))
            {
                _attackTime = clip.length / 2;
                break;
            }
        }
        health = this.GetComponent<Health>();
    }

    //this gets called by the Player Input Component on this object
    public void OnLocomotion(InputValue value)
    {
       _movementInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (_isSliding || _isAttacking)
            return;
        _lookDirection = value.Get<Vector2>();
        if (_lookDirection.magnitude > 0)
        {
            _lookDirectionIndex = (int)HelperFunctions.CardinalizeVector(_lookDirection);
            foreach (Animator _playerAnimator in _playerAnimators)
                _playerAnimator.SetInteger("LookDirection", _lookDirectionIndex);
        }
        
    }

    public void OnSlide()
    {
        if(!_canSlide || health.IsStunned)
            return;
        _playerCanTakeDamageEvent.Invoke(data: false);
        _isSliding = true;
        _canSlide = false;
        DOTween.Sequence()
            .InsertCallback(_invincibilityLength, () => _playerCanTakeDamageEvent.Invoke(data: true))
            .InsertCallback(_slideLength, () => _isSliding = false)//stopping the slide
            .AppendInterval(_slideCoolDown)
            .AppendCallback(() => _canSlide = true);//allows to slide again
        //adds an impulse force either in the current movement direction or if standing still, in the direction they are facing
        _rb.AddForce((_rb.velocity.magnitude > 0? _rb.velocity.normalized : _cardinalMoveDirection.normalized) * _slideSpeed, ForceMode.Impulse);
        _lookDirectionIndex = (int)HelperFunctions.CardinalizeVector(_rb.velocity);
        foreach(Animator _playerAnimator in _playerAnimators)
            _playerAnimator.SetInteger("LookDirection", _lookDirectionIndex);
    }

    public void OnAttack()
    {
        if (_isAttacking || _isSliding || health.IsStunned || !_canAttack)
            return;
        _isAttacking = true;
        _canAttack = false;
        int hitboxIndex = _lookDirectionIndex;
        _hitboxes[hitboxIndex].SetActive(true);
        DOTween.Sequence()
            .InsertCallback(_attackTime, () =>
            {
                _isAttacking = false;
                _hitboxes[hitboxIndex].SetActive(false);
            })
            .InsertCallback(_attackCoolDown, () => _canAttack = true);
    }

    private void FixedUpdate()
    {
        if (health.IsStunned)
            return;
        currentOrientation = _mainCamera.transform.forward;
        currentOrientation.y = 0;
        currentOrientation.Normalize();
        _movementDirection = (this.transform.forward * _movementInput.y) + (this.transform.right * _movementInput.x);
        if(_movementDirection.magnitude > 1)
        {
            _movementDirection.Normalize();
        }
        this.transform.forward = currentOrientation;
        if(!_isSliding)
        {
            _rb.velocity = _movementDirection * _movementSpeed;
        }
        if(_rb.velocity.magnitude != 0)
        {
            _cardinalMoveDirection = HelperFunctions.VectorDirections[(int)HelperFunctions.CardinalizeVector(_rb.velocity)];
        }
        //animator updates
        foreach(Animator _playerAnimator in _playerAnimators)
        {
            _playerAnimator.SetFloat("Speed", _rb.velocity.magnitude);
            _playerAnimator.SetBool("isSliding", _isSliding);
            _playerAnimator.SetBool("isAttacking", _isAttacking);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Health>().AffectHealth(null, -_baseDamage);
        }
    }
}
