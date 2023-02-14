using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]private Health _entityHealth;
    [SerializeField]private Image _healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _healthBar.fillAmount = _entityHealth.GetHealthPercentage();
    }
}
