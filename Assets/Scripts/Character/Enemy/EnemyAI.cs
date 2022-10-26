using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("---Movement---")]
    public bool canMove = true;

    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField] float moveSpeed = 5f;

    /// <summary>
    /// 敌人的模型对象，用来修改缩放以达到转向的目的
    /// </summary>
    [SerializeField] Transform model;

    [Header("---PathFinding---")]
    /// <summary>
    /// 到达下一个路点的阈值，小于这个阈值则切换到下一个路点
    /// </summary>
    [SerializeField] float nextWayPointDistance = 2f;

    /// <summary>
    /// 追踪的目标
    /// </summary>
    [DisplayOnly][SerializeField] Vector3 targetPosition;

    [Header("---AILogic---")]
    [SerializeField] float checkPlayerRadius = 8f;

    [SerializeField] float chasePlayerRadius = 6f;

    [SerializeField] float attackPlayerRadius = 2f;

    [SerializeField] float nextGuardWayPointDistance = 2f;

    [SerializeField] float guardWaitDuration = 2f;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] Transform[] guardWayPoints;

    public float waitAfterAttackTime = 1f;

    [DisplayOnly][SerializeField] float guardWaitTime = 2f;

    Vector3[] initialGuardWayPoints;

    /// <summary>
    /// 寻路获取的路径
    /// </summary>
    Path path;

    EnemyManager EM;

    Collider2D[] overlapResults = new Collider2D[1];

    [DisplayOnly][SerializeField] Transform player;

    /// <summary>
    /// 当前路点索引值
    /// </summary>
    int currentWayPoint = 0;

    int guardWayPointIndex = 0;

    Seeker seeker;

    Rigidbody2D rb;

    private void Awake()
    {
        InitializeObject();
    }

    private void OnEnable()
    {
        InitializeGuardWayPoints();
    }

    private void Start()
    {
        StartPathFinding();
    }

    private void FixedUpdate()
    {
        HandlePath();
    }

    void InitializeGuardWayPoints()
    {
        initialGuardWayPoints = new Vector3[guardWayPoints.Length];
        for (int i = 0; i < guardWayPoints.Length; i++)
        {
            initialGuardWayPoints[i] = guardWayPoints[i].position;
        }
    }

    void InitializeObject()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        EM = GetComponent<EnemyManager>();
        player = GameObject.FindGameObjectWithTag("PlayerCheck").transform;
    }

    /// <summary>
    /// 开始路径计算
    /// </summary>
    public void StartPathFinding()
    {
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    /// <summary>
    /// /// 取消路径计算
    /// </summary>
    public void StopPathFinding()
    {
        CancelInvoke("UpdatePath");
    }

    /// <summary>
    /// 上一条路径生成完成之后生成新的路径
    /// </summary>
    void UpdatePath()
    {
        if (!canMove)
        {
            return;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        }
    }

    /// <summary>
    /// 路径完成后的回调函数
    /// </summary>
    /// <param name="p"></param>
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    /// <summary>
    /// 处理路径，根据路点来基于敌人速度
    /// </summary>
    void HandlePath()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        if (path == null)//路径为空返回
        {
            return;
        }
        Debug.DrawLine(transform.position, targetPosition, Color.red);
        currentWayPoint = Mathf.Clamp(currentWayPoint, 0, path.vectorPath.Count - 1);//放置索引值超出范围
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;//根据路点计算出即将运动的方向
        Vector2 force = direction * moveSpeed;//生成速度
        rb.velocity = new Vector2(force.x, rb.velocity.y);//给速度赋值
        HandleOrientation();

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);//目标路点与敌人的距离

        if (distance < nextWayPointDistance)//小于距离阈值，则前往下一个路点
        {
            currentWayPoint++;
        }
    }

    /// <summary>
    /// 处理角色转向
    /// </summary>
    public void HandleOrientation()
    {
        if (targetPosition == null)
        {
            return;
        }
        if ((targetPosition - transform.position).x >= 0)
        {
            model.localScale = new Vector3(-1, 1, 1);
        }
        else if ((targetPosition - transform.position).x < 0)
        {
            model.localScale = Vector3.one;
        }
    }

    public void EnableAIMove()
    {
        canMove = true;
    }

    public void DisableAIMove()
    {
        canMove = false;
    }

    public void SetTarget()
    {
        targetPosition = player.position;
    }

    /// <summary>
    /// 检查玩家是否在检查范围内（巡逻状态下）
    /// </summary>
    /// <returns></returns>
    public bool CheckPlayerAround_Guard()
    {
        if (Physics2D.OverlapCircleNonAlloc(transform.position, checkPlayerRadius, overlapResults, playerLayer) > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 检查玩家是否在检查范围内（追击范围内）
    /// </summary>
    /// <returns></returns>
    public bool CheckPlayerAround_Chase()
    {
        if (Physics2D.OverlapCircleNonAlloc(transform.position, chasePlayerRadius, overlapResults, playerLayer) > 0)
        {
            SetTarget();
            return true;
        }
        return false;
    }

    public bool CheckRadiusForAttack()
    {
        if (Physics2D.OverlapCircleNonAlloc(transform.position, attackPlayerRadius, overlapResults, playerLayer) > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 物理模拟关闭，不会产生碰撞效果
    /// </summary>
    public void ShutDownPhysics()
    {
        rb.simulated = false;
    }

    /// <summary>
    /// 开启巡逻携程
    /// </summary>
    public void StartPatrolCoroutine()
    {
        StartCoroutine(nameof(PatrolCoroutine));
    }

    /// <summary>
    /// 停止巡逻携程
    /// </summary>
    public void StopPatrolCoroutine()
    {
        StartCoroutine(nameof(PatrolCoroutine));
    }

    /// <summary>
    /// 巡逻逻辑处理
    /// </summary>
    IEnumerator PatrolCoroutine()
    {
        //设置目标为第一个巡逻点
        targetPosition = initialGuardWayPoints[guardWayPointIndex];
        while (true)
        {
            // //如果AI与目标巡逻点的距离小于巡逻点切换阈值，则停止一会，然后移动道下一个巡逻点
            if (Vector2.Distance(transform.position, targetPosition) <= nextGuardWayPointDistance)
            {
                DisableAIMove();
                if (!EM.enemyStats.Dead)
                {
                    EM.enemyAnimatorHandler.animator.CrossFade("Idle", 0.2f);
                }
                yield return new WaitForSeconds(2f);
                if (!EM.enemyStats.Dead)
                {
                    EM.enemyAnimatorHandler.animator.CrossFade("Move", 0.2f);
                }
                EnableAIMove();
                guardWaitTime = guardWaitDuration;
                guardWayPointIndex++;
                if (guardWayPointIndex >= initialGuardWayPoints.Length)
                {
                    guardWayPointIndex = 0;
                }
                targetPosition = initialGuardWayPoints[guardWayPointIndex];
            }
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkPlayerRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chasePlayerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackPlayerRadius);
    }
}
