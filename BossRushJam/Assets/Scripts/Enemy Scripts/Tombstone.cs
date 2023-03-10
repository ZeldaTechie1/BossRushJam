using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : Projectile
{
    [SerializeField]
    private SpriteRenderer _shadowRenderer;
    [SerializeField]
    private SpriteRenderer _tombstoneRenderer;

    private List<Collider> _hitColliders = new List<Collider>();
    private void Awake()
    {
        _collider= GetComponent<Collider>();
        _shadowRenderer.material.color = Color.clear;
    }

    public override void HandleCollision(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null || !health.CanTakeDamage) { return; }
        if (_hitColliders.Contains(other)) { return; }
        _hitColliders.Add(other);
        health.AffectHealth(null, -_damage);
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        DOTween.Sequence().SetDelay(1).AppendCallback(() => {if (other == null) { return; } other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }

    public override void Launch(GameObject target)
    {
        Vector3 originalPos = _tombstoneRenderer.transform.position;
        Vector3 launch = _tombstoneRenderer.transform.position + transform.forward * 5f;
        launch.y += 50;
        DOTween.Sequence().Append(_tombstoneRenderer.transform.DOMove(launch, 1f)).AppendCallback(() =>
        {
            _tombstoneRenderer.transform.position = new Vector3(originalPos.x, _tombstoneRenderer.transform.position.y, originalPos.z);
            transform.position = target.transform.position;
            _shadowRenderer.material.DOFade(0.75f, 0.5f);
        }).AppendInterval(0.25f).Append(_tombstoneRenderer.transform.DOLocalMoveY(0, 0.25f)).AppendCallback(() =>
        {
            _shadowRenderer.material.DOFade(0, 1f);
            _tombstoneRenderer.material.DOFade(0, 1f);
            _collider.enabled = true;
        }).AppendInterval(0.25f).AppendCallback(() => { _collider.enabled = false; }).AppendInterval(1f).OnComplete(() =>
        {
            Destroy(gameObject);
        }); 
    }
}
