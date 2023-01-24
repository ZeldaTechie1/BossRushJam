using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]private Camera _mainCamera;
    [SerializeField]private float _movementSpeed;

    private Rigidbody _rb;
    private Vector3 _movementDirection;
    private Vector2 _movementInput;

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

    private void FixedUpdate()
    {
        Vector3 currentOrientation = _mainCamera.transform.forward;
        currentOrientation.y = 0;
        currentOrientation.Normalize();
        _movementDirection = (this.transform.forward * _movementInput.y) + (this.transform.right * _movementInput.x);
        this.transform.forward = currentOrientation;
        _rb.velocity = _movementDirection * _movementSpeed;
    }

}
