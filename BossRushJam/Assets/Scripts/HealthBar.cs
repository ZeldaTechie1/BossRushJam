using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]private Health _entityHealth;
    [SerializeField]private Image _healthBar;

    private Sequence _sequence;
    // Start is called before the first frame update
    void Start()
    {
        _entityHealth.HealthAffected += AdjustHealth;
        Boss boss = _entityHealth.GetComponent<Boss>();
        if (boss)
        {
            boss.SpawnNextBoss += ChangeTarget;
        }
    }

    private void AdjustHealth()
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence().Append(_healthBar.DOFillAmount(_entityHealth.GetHealthPercentage(), 0.25f));
    }

    public void ChangeTarget()
    {
        Boss nextTarget = _entityHealth.GetComponent<Boss>().NextBoss;
        if (nextTarget != null)
        {
            _entityHealth.HealthAffected -= AdjustHealth;
            _entityHealth.GetComponent<Boss>().SpawnNextBoss -= ChangeTarget;
            _entityHealth = nextTarget.GetComponent<Health>();
            _entityHealth.HealthAffected += AdjustHealth;
            nextTarget.SpawnNextBoss += ChangeTarget;
            AdjustHealth();
        }
    }
}
