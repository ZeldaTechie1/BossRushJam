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
        Health health = other.GetComponent<Health>();
        if (!health.CanTakeDamage) { return; }
        if (_hitColliders.Contains(other)) { return; }
        _hitColliders.Add(other);
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        health.AffectHealth(null, -15f);
        health.IsStunned = true;
        DOTween.Sequence().SetDelay(0.5f + 1f - _moveSequence.ElapsedPercentage()).AppendCallback(() => { if (health == null) { return; } health.IsStunned = false; });

        other.GetComponent<Rigidbody>().AddForce(transform.forward * 20 * (2 - _moveSequence.ElapsedPercentage()), ForceMode.Impulse);
        DOTween.Sequence().SetDelay(0.5f + 1 - _moveSequence.ElapsedPercentage()).AppendCallback(() => { if (other == null) { return; } other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }

    public override void Launch(GameObject target)
    {
        _moveSequence = DOTween.Sequence().Append(transform.DOMove(target.transform.position, 2).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); }));
    }
}
