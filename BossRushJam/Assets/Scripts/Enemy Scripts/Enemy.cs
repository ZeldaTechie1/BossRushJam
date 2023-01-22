using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    [SerializeField]private float _enemySpeed;
    [SerializeField]private float _attackTime;
    [SerializeField]private float _attackHurtCheck;//eh idk about this but it's something
    [SerializeField]private float _attackDistance;
    [SerializeField]private GameEvent HurtPlayer;

    private Rigidbody _rb;
    private GameObject _player;
    private bool _isAttacking;
    private Vector3 _playerPosition;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        if(_enemySpeed == 0)
        {
            Debug.LogWarning("Enemy speed is set to 0. Is this correct?");
        }
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if(_player == null)
        {
            throw new System.Exception("Player does not exist! What is my purpose?");
        }
        if(_isAttacking)
        {
            return;
        }
        //move towards player
        _playerPosition = _player.transform.position;
        _playerPosition.y = this.transform.position.y;//this ensures that there are no height discrepancies between the player and enemy
        Vector3 directionToMove = _player.transform.position - this.transform.position;//c = b-a
        directionToMove.Normalize();
        _rb.velocity = directionToMove.normalized * _enemySpeed;
        if(Vector3.Distance(_playerPosition,this.transform.position) <= _attackDistance)
        {
            //Attack();
        }
    }

    private void Attack()
    {
        _isAttacking = true;
        //check if the player is still in range after a certain amount of time
        //TODO: this could definitely be done better but I just want it to work lol
        DOTween.Sequence().InsertCallback(_attackHurtCheck, () =>
        {
            if(Vector3.Distance(_playerPosition, this.transform.position) <= _attackDistance)//if the player is still in range when this happens then we can hurt the player
            {
                HurtPlayer.Invoke();
            }
        });
        DOTween.Sequence().InsertCallback(_attackTime, () => _isAttacking = false);//might be a better way to do this but not worth looking into just yet
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

}
