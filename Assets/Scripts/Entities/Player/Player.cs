using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public PlayerCondition Condition;
    public PlayerAttackController Attack;
    public PlayerMovementController Movement;

    private void Awake()
    {
        Condition = GetComponent<PlayerCondition>();
        Attack = GetComponent<PlayerAttackController>();
        Movement = GetComponent<PlayerMovementController>();
    }
}
