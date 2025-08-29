using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Configuration")]
    private Player player;

    private bool canAttack = true;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    public void PerformAttack(IDamagable target)
    {
        if (canAttack)
        {
            if (target != null)
            {
                target.TakeDamage(player.Condition.totalAttackDamage);

                StartCoroutine(AttackCooldownCoroutine());
            }
        }
    }
    private IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(player.Condition.totalAttackInterval);
        canAttack = true;
    }
}
