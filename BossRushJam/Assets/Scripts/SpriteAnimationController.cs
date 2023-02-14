using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationController : MonoBehaviour
{
    [SerializeField]private Animator _spriteAnimator;
    [SerializeField]private Rigidbody _rb;

    private void Start()
    {
        if(_spriteAnimator == null)
        {
            _spriteAnimator = GetComponent<Animator>();
        }
        if(_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        //calculate the movement of the sprite
        Vector3 movement = _rb.velocity;
        _spriteAnimator.SetInteger("LookDirection", (int)HelperFunctions.CardinalizeVector(movement));
    }
}
