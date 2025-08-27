using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Configuration")]
    public int attackDamage = 10;
    public float attackInterval = 1f;

    private bool canAttack = true;

    public void PerformAttack(IDamagable target)
    {
        if (canAttack)
        {
            if (target != null)
            {
                target.TakeDamage(attackDamage);

                StartCoroutine(AttackCooldownCoroutine());
            }
        }
    }
    private IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }
}
