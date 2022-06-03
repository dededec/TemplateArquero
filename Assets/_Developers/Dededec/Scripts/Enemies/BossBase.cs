using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBase : EnemyBase
{
    [Header("Boss settings")]
    [SerializeField] private Image _slider; 

    // Start is called before the first frame update
    void Start()
    {
        _slider.fillAmount = 1;
    }

    public override void TakeDamage(int amount)
    {
        _health -= amount;
        _slider.fillAmount = (float)_health/_maxHealth;
        if(_health <= 0)
        {
            // Destruir cositas
            Destroy(this.gameObject);
        }
    }
}
