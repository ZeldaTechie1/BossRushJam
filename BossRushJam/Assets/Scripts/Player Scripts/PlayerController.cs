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
    [SerializeField]private Animator _playerAnimator;

    private Rigidbody _rb;
    private Vector2 _movementInput;
    private Vector2 _lookDirection;
    private Vector3 _movementDirection;
    private Vector3 _cardinalMoveDirection;
    private Vector3 currentOrientation;
    private bool _isSliding;
    private bool _isAttacking;
    private bool _canSlide = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    //this gets called by the Player Input Component on this object
    public void OnLocomotion(InputValue value)
    {
       _movementInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (_isSliding)
            return;
        _lookDirection = value.Get<Vector2>();
        if(_lookDirection.magnitude > 0)
        {
            _playerAnimator.SetInteger("LookDirection", (int)HelperFunctions.CardinalizeVector(_lookDirection));
        }
        
    }

    public void OnSlide()
    {
        if(!_canSlide)
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
        _playerAnimator.SetInteger("LookDirection", (int)HelperFunctions.CardinalizeVector(_rb.velocity));
    }

    public void OnAttack()
    {

    }

    private void FixedUpdate()
    {
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
        _playerAnimator.SetFloat("Speed", _rb.velocity.magnitude);
        _playerAnimator.SetBool("isSliding", _isSliding);
    }

}
