using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationController : MonoBehaviour
{
    [SerializeField]private Animator _spriteAnimator;

    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = this.transform.position;
        _spriteAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //calculate the movement of the sprite
        Vector3 movement = this.transform.position - previousPosition;
        if(movement != Vector3.zero)
        {
            movement.Normalize();
        }
        _spriteAnimator.SetFloat("SpeedX", movement.x);
        _spriteAnimator.SetFloat("SpeedY", movement.z);
        _spriteAnimator.SetFloat("Speed", movement.magnitude);
        previousPosition = this.transform.position;
    }
}
