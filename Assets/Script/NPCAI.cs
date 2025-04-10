using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// author: Kehan Gong
/// Enemy AI searching player

public class NPCAI : MonoBehaviour
{
    
    [Header("巡逻设置")]
    public float patrolRadius = 5f;      // 巡逻半径
    public float patrolSpeed = 2f;      // 巡逻速度
    public float patrolInterval = 3f;   // 巡逻间隔

    [Header("追逐设置")]
    public float chaseSpeed = 4f;       // 追逐速度
    public float detectionRadius = 8f;  // 检测半径
    public float chaseStopDistance = 1f;// 停止追逐距离
    public float stopTime = 2f;        // 停止时间

    private Transform player;           // 玩家对象
    private Vector2 patrolPoint;        // 当前巡逻点
    private bool isChasing = false;     // 是否正在追逐
    private float patrolTimer;          // 巡逻计时器
    private float stopTimer = 0f;           // 停止计时器
    private float previousSpeed = 0f;       // 之前速度
    public CinemachineImpulseSource impulseSource; // Impulse Source 组件
    private bool isStopped = false;       // 是否停止
    private SpriteRenderer spriteRenderer; // 翻转精灵
    private Animator animator;

    void Start()
    {
        // 查找玩家对象
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 初始化巡逻点
        GetNewPatrolPoint();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // <summary>
    /// 自动根据朝向翻转
    /// </summary>
    void FlipTowards(Vector2 target)
    {
        float direction = target.x - transform.position.x;

        if (Mathf.Abs(direction) > 0.01f)
            spriteRenderer.flipX = direction > 0;
    }
    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
            if (isStopped)
            {
                stopTimer += Time.deltaTime;

                // 恢复追逐速度
                if (stopTimer >= stopTime)
                {
                    stopTimer = 0f;
                    chaseSpeed = previousSpeed;
                    isStopped = false;
                }
            }
        }
        else
        {
            Patrol();
            CheckForPlayer();
        }
        float currentSpeed = isChasing ? chaseSpeed : patrolSpeed;

        // 翻转角色朝向
        Vector2 target = isChasing ? player.position : patrolPoint;
        FlipTowards(target);
    }
    private void OnTriggerEnter2D(Collider2D other)
        {
            // 检查触发对象是否是玩家
            if (other.CompareTag("Player"))
            {
                Debug.Log("NPC hit the player!");

                if (impulseSource != null)
                {
                    impulseSource.GenerateImpulse();
                }

                // 停止追击
                if (!isStopped)
                {
                    previousSpeed = chaseSpeed;
                    chaseSpeed = 0f;
                    isStopped = true;
                }
            }
        }

    /// <summary>
    /// 巡逻逻辑
    /// </summary>
    void Patrol()
    {
        // 移动到巡逻点
        transform.position = Vector2.MoveTowards(transform.position, patrolPoint, patrolSpeed * Time.deltaTime);

        // 到达巡逻点后生成新点
        if (Vector2.Distance(transform.position, patrolPoint) < 0.1f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolInterval)
            {
                
                GetNewPatrolPoint();
                patrolTimer = 0f;
            }
        }
    }

    /// <summary>
    /// 生成新的巡逻点
    /// </summary>
    void GetNewPatrolPoint()
    {
        patrolPoint = (Vector2)transform.position + Random.insideUnitCircle * patrolRadius;
    }

    /// <summary>
    /// 检测玩家是否进入视野
    /// </summary>
    void CheckForPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            isChasing = true;
        }
    }

    /// <summary>
    /// 追逐玩家
    /// </summary>
    void ChasePlayer()
    {
        // 移动到玩家位置
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // 如果玩家离开检测范围，停止追逐
        if (Vector2.Distance(transform.position, player.position) > detectionRadius)
        {
           
            isChasing = false;
            GetNewPatrolPoint(); // 返回巡逻状态
        }
    }

    /// <summary>
    /// 可视化调试
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // 绘制巡逻范围
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        // 绘制检测范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
