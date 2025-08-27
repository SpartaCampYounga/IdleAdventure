using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Configuration")]
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float rotationSpeed = 5f;

    private PlayerState currentState = PlayerState.Idle;
    private Rigidbody _rigidbody;
    private PlayerAttackController attackController;

    [Header("Enemies")]
    [SerializeField ]private List<GameObject> enemyTargets;  //GameManager에서 Stage 변경될때 지정해주기. 일단은 SerializeField 설정
    public Transform targetEnemy;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;   //넘어짐 방지
        attackController = GetComponent<PlayerAttackController>();
    }

    private void Update()
    {
        if(targetEnemy == null)
        {
            FindNewTargetFromList();
        }
        else
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetEnemy.position);

            if (distanceToTarget < attackRange)
            {
                currentState = PlayerState.Attack;
            }
            else
            {
                currentState = PlayerState.Move;
            }

        }
    }

    private void FixedUpdate()  //rigidbody 
    {
        if (targetEnemy == null)
        {
            currentState = PlayerState.Idle;
            _rigidbody.velocity = Vector3.zero; 
            return;
        }

        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        switch (currentState)
        {
            case PlayerState.Idle:
                _rigidbody.velocity = Vector3.zero;
                break;
            case PlayerState.Move:
                //높이 속도 유지하면서 이동하기. //음.. 점프 기능 없는데 필요한가?
                Vector3 moveVelocity = (targetEnemy.position - transform.position).normalized * moveSpeed;
                moveVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = moveVelocity;

                break;
            case PlayerState.Attack:
                _rigidbody.velocity = Vector3.zero;
                if(targetEnemy != null)
                {
                    IDamagable damagableTarget = targetEnemy.gameObject.GetComponent<IDamagable>();
                    attackController.PerformAttack(damagableTarget);
                }
                break;
        }
    }

    public void SetEnemyTargetList(List<GameObject> enemies)
    {
        this.enemyTargets = enemies;
        Debug.Log($"Player: {this.enemyTargets.Count}마리 적 리스트를 받았습니다.");
        FindNewTargetFromList();
    }

    private void FindNewTargetFromList()
    {
        //if (enemyTargets == null || enemyTargets.Count == 0)
        //{
        //    //todo: Stage Clear
        //    return;
        //}

        GameObject closestTargetObject = null;
        float closestDistance = Mathf.Infinity; //Max값을 무한으로 일단 지정

        for (int i = 0; i < enemyTargets.Count; i++)
        {
            if (enemyTargets[i] == null)
            {
                enemyTargets.RemoveAt(i);
                i--;    //제거한만큼 당기기
                continue;
            }

            float distance = Vector3.Distance(transform.position, enemyTargets[i].transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestTargetObject = enemyTargets[i];
            }
        }

        targetEnemy = closestTargetObject != null ? closestTargetObject.transform : null;
    }
}
