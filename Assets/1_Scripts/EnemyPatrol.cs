using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;

    private int currentPatrolIndex = 0;
    private Vector3 currentTarget;

    void Start()
    {
        // 設置初始目標為第一個巡邏點
        currentTarget = patrolPoints[currentPatrolIndex].position;
    }

    void Update()
    {
        // 計算到目標點的距離
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget);

        // 如果到達目標點，則設置下一個目標點
        if (distanceToTarget < 0.1f)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }
            currentTarget = patrolPoints[currentPatrolIndex].position;
        }

        // 計算敵人需要移動的方向
        Vector3 direction = (currentTarget - transform.position).normalized;

        // 計算敵人需要轉向的角度
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // 漸進式地旋轉敵人
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        // 設置敵人的移動速度和方向
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
