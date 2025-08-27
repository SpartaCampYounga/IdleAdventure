using System;
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
        OnHPChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        Debug.Log($"Player (이)가 {damage}를 입어 체력이 {currentHealth} / {maxHealth}가 되었다.");

        OnHPChanged?.Invoke();
    }

    public void GetHealed(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        Debug.Log($"Player (이)가 {amount}를 회복하여 체력이 {currentHealth} / {maxHealth}가 되었다.");

        OnHPChanged?.Invoke();
    }
}
