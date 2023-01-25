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

    private Rigidbody _rb;
    private Vector3 _movementDirection;
    private Vector3 _cardinalDirection;
    private Vector2 _movementInput;
    private bool _isSliding;
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

    public void OnSlide()
    {
        if(!_canSlide)
            return;
        _isSliding = true;
        _canSlide = false;
        DOTween.Sequence()
            .InsertCallback(_slideLength, () => _isSliding = false)//stopping the slide
            .AppendInterval(_slideCoolDown)
            .AppendCallback(() => _canSlide = true);//allows to slide again
        //adds an impulse force either in the current movement direction or if standing still, in the direction they are facing
        _rb.AddForce((_rb.velocity.magnitude > 0? _rb.velocity.normalized : _cardinalDirection.normalized) * _slideSpeed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Vector3 currentOrientation = _mainCamera.transform.forward;
        currentOrientation.y = 0;
        currentOrientation.Normalize();
        _movementDirection = (this.transform.forward * _movementInput.y) + (this.transform.right * _movementInput.x);
        this.transform.forward = currentOrientation;
        if(!_isSliding)
        {
            _rb.velocity = _movementDirection * _movementSpeed;
        }
        if(_rb.velocity.magnitude != 0)
        {
            GetCardinalDirection();
        }
        
    }

    private void GetCardinalDirection()
    {
        Vector3[] directions = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        float maxDot = -Mathf.Infinity;
        _cardinalDirection = Vector3.zero;

        foreach(Vector3 direction in directions)
        {
            float dotProduct = Vector3.Dot(_rb.velocity, direction);
            if (dotProduct > maxDot)
            {
                maxDot = dotProduct;
                _cardinalDirection = direction;
            }
        }
    }

}
