using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    [SerializeField]private float _enemySpeed;
    [SerializeField]private float _attackPower;
    [SerializeField]private float _attackLength;
    [SerializeField]private float _attackHurtCheck;//eh idk about this but it's something
    [SerializeField]private float _attackDistance;
    [SerializeField]private GameEvent HurtPlayer;
    [SerializeField]private float _attackDistanceBuffer;
    [SerializeField]private Renderer _renderer;
    [SerializeField]private Animator _spriteAnimator;

    private Rigidbody _rb;
    private GameObject _player;
    private bool _isAttacking;
    private Vector3 _playerPosition;
    private Health health;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        if(_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }
        _rb.freezeRotation = true;
        if(_enemySpeed == 0)
        {
            Debug.LogWarning("Enemy speed is set to 0. Is this correct?");
        }
        health = this.GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
        _spriteAnimator.SetBool("isAttacking", _isAttacking);
    }

    private void MoveTowardsPlayer()
    {
        if(_player == null)
        {
            return;
        }
        _playerPosition = _player.transform.position;
        _playerPosition.y = this.transform.position.y;//this ensures that there are no height discrepancies between the player and enemy
        if (_isAttacking || health.IsStunned)
        {
            return;
        }
        if (Vector3.Distance(_playerPosition, this.transform.position) <= (_attackDistance - _attackDistanceBuffer))
        {
            Attack();
            return;
        }
        //move towards player
        _renderer.material.color = Color.white;
        Vector3 directionToMove = _player.transform.position - this.transform.position;//c = b-a
        directionToMove.Normalize();
        _rb.velocity = directionToMove.normalized * _enemySpeed;
    }

    private void Attack()
    {
        if (health.IsStunned)
            return;
        _isAttacking = true;
        _renderer.material.color = Color.yellow;
        //check if the player is still in range after a certain amount of time
        //TODO: this could definitely be done better but I just want it to work lol
        DOTween.Sequence().InsertCallback(_attackHurtCheck, () =>
        {
            if (this == null)
                return;
            _renderer.material.color = Color.red;
            Vector3 instancedPlayerPosition = _playerPosition;
            if(Vector3.Distance(instancedPlayerPosition, this.transform.position) <= _attackDistance)//if the player is still in range when this happens then we can hurt the player
            {
                HurtPlayer.Invoke(data: -_attackPower);
            }
        });
        DOTween.Sequence().InsertCallback(_attackLength, () => _isAttacking = false);//might be a better way to do this but not worth looking into just yet
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

}
