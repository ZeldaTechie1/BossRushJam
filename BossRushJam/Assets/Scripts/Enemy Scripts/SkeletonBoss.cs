using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : Boss
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CheckForPhaseChange(true);
        }
        if (!IsMoving || _health.IsDead)
        {
            return;
        }
        if (_currentAttack < 0)
        {
            SetAttack();
        }
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        if (_currentPhase == 1)
        {
        }
        if (_currentPhase == _maxPhase)
        {
            _animator.SetTrigger("4ArmMode");
        }
    }

    private void Stab2H()
    {

    }

    private void Slash2H()
    {

    }

    private void Stab4H()
    {

    }

    private void Slash4H()
    {

    }

    private void SpinAttack()
    {

    }
}
