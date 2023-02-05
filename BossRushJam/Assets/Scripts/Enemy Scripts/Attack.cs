using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        DOTween.Sequence().SetDelay(1).AppendCallback(() => { other.GetComponentInChildren<SpriteRenderer>().color = Color.white; });
    }
}
