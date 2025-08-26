using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Configuration")]
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float rotationSpeed = 5f;
    public Transform targetEnemy;

    private PlayerState currentState = PlayerState.Move;
    private Rigidbody _rigidbody;
    private AttackController attackController;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;   //넘어짐 방지
        attackController = GetComponent<AttackController>();
    }

    private void FixedUpdate()  //rigidbody 
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                _rigidbody.velocity = Vector3.zero;
                break;
            case PlayerState.Move:
                if (Vector3.Distance(transform.position, targetEnemy.position) < attackRange)
                {
                    //To do: Attack으로 바꾸기
                    _rigidbody.velocity = Vector3.zero;
                    currentState = PlayerState.Idle;
                    //currentState = PlayerState.Attack;
                }

                Vector3 direction = (targetEnemy.position - transform.position).normalized;
                direction.y = 0;

                //높이 속도 유지하면서 이동하기. //음.. 점프 기능 없는데 필요한가?
                Vector3 moveVelocity = direction * moveSpeed;
                moveVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = moveVelocity;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

                break;
            case PlayerState.Attack:
                _rigidbody.velocity = Vector3.zero;
                if(targetEnemy != null)
                {
                    //To do. AttackController에서 PerformAttack(targetEnemy)
                }
                //To do. 만약 Enemy가 죽으면? targetEnemy null처리, currentState Idle.
                break;
        }
    }
}
