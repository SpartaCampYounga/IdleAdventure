using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    [Header("Condition")]
    public int currentHealth;
    public int maxHealth = 100;

    public Action OnHPChanged;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        Debug.Log($"Player (이)가 {damage}를 입어 체력이 {currentHealth} / {maxHealth}가 되었다.");

        if(OnHPChanged != null)
        {
            OnHPChanged();
        }
    }
}
