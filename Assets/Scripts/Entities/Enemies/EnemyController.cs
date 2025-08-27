using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    [Header("Configuration")]
    private static int nextId = 1;
    public int id;

    [Header("Status")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public int attactDamage = 10;
    public float attactRange = 2f;
    public float attackInterval = 1f;
    
    [Header("Condition")]
    public int currentHealth;
    public int maxHealth = 30;

    [Header("Reward")]
    public int rewardGold = 10;
    public int rewardEXP = 30;

    private Rigidbody _rigidbody;
    private Transform playerTarget;
    private bool canAttack = true;

    private void Awake()
    {
        id = nextId++;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        PlayerCondition player = FindAnyObjectByType<PlayerCondition>();
        if (player != null) 
        {
            playerTarget = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (playerTarget == null)
        {
            //플레이어 없으므로 Idle
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if(distanceToPlayer < attactRange)  //공격범위 내
        {
            _rigidbody.velocity = Vector3.zero;
            PerformAttack();
        }
        else //공격 범위 밖
        {
            Vector3 direction = (playerTarget.position - transform.position).normalized;
            direction.y = 0;

            _rigidbody.velocity = direction * moveSpeed;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void PerformAttack()
    {
        if(canAttack)
        {
            IDamagable damagable = playerTarget.GetComponent<IDamagable>();
            if (damagable != null && damagable is PlayerCondition)
            {
                damagable.TakeDamage(attactDamage);

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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{id}(이)가 {damage}를 입어 체력이 {currentHealth} / {maxHealth}가 되었다.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{id}(이)가 처치되었습니다.");
        RewardPlayer();
        Destroy(gameObject);    //to do: 오브젝트 풀링 배우면 Destory 대신에 오브젝트 풀링 쓰기.
    }

    void RewardPlayer()
    {
        GameManager.Instance.EarnGold(rewardGold);
        GameManager.Instance.EarnEXP(rewardEXP);
    }
}
