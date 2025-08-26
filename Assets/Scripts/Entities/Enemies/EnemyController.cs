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
    
    [Header("Condition")]
    public int currentHealth;
    public int maxHealth = 100;

    private void Awake()
    {
        id = nextId++;
    }
    private void Start()
    {
        currentHealth = maxHealth;
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
        //to do: 재화/경험치 획득
        Destroy(gameObject);    //to do: 오브젝트 풀링 배우면 Destory 대신에 오브젝트 풀링 쓰기.
    }
}
