using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(SimpleSmoothModifier))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyManager : MonoBehaviour
{

    [DisplayOnly] public EnemyStats enemyStats;

    [DisplayOnly] public EnemyAnimatorHandler enemyAnimatorHandler;

    [DisplayOnly] public EnemyStateMachine enemyStateMachine;

    [DisplayOnly] public EnemyAI enemyAI;

    [DisplayOnly] public EnemyAttackHandler enemyAttackHandler;

    [DisplayOnly] public EnemySFXHandler enemySFXHandler;

    [DisplayOnly] public CameraHandler cameraHandler;

    [DisplayOnly] public EnemyDropHandler enemyDropHandler;

    private void Awake()
    {
        InitializeObject();
    }

    /// <summary>
    /// 初始化对象
    /// </summary>
    void InitializeObject()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        enemyAI = GetComponent<EnemyAI>();
        enemyAttackHandler = GetComponentInChildren<EnemyAttackHandler>();
        enemySFXHandler = GetComponentInChildren<EnemySFXHandler>();
        cameraHandler = GetComponentInChildren<CameraHandler>();
        enemyDropHandler = GetComponentInChildren<EnemyDropHandler>();
    }
}
