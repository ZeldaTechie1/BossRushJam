using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shockwave : Projectile
{
    private Sequence _moveSequence;
    private List<Collider> _hitColliders = new List<Collider>();
    public override void HandleCollision(Collider other)
    {
        if (_hitColliders.Contains(other)) { return; }
        _hitColliders.Add(other);
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        if (other.tag == "Player")
        { 
            PlayerController player = other.GetComponent<PlayerController>();
            player.GetComponent<PlayerInput>().enabled = false;
            player.enabled = false;
            DOTween.Sequence().SetDelay(0.5f + 1f - _moveSequence.ElapsedPercentage()).AppendCallback(() => { player.enabled = true; player.GetComponent<PlayerInput>().enabled = true; });
        }
        other.GetComponent<Rigidbody>().AddForce(transform.forward * 20 * (2 - _moveSequence.ElapsedPercentage()), ForceMode.Impulse);
        DOTween.Sequence().SetDelay(0.5f + 1 - _moveSequence.ElapsedPercentage()).AppendCallback(() => { other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }

    public override void Launch(GameObject target)
    {
        _moveSequence = DOTween.Sequence().Append(transform.DOMove(target.transform.position, 2).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); }));
    }
}
