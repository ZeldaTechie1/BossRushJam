using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Projectile
{

    public override void HandleCollision(Collider other)
    {
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        DOTween.Sequence().SetDelay(1).AppendCallback(() => { other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }

    public override void Launch(Vector3 target)
    {
        DOTween.Sequence().AppendCallback(() =>
        {
            transform.DOMove(target, 2).OnComplete(() => { Destroy(this.gameObject); });
        });
    }
}
