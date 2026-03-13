using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float followRange = 10f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    public List<Transform> patrolPoints;
    private Transform playerTransform;
    bool stopPatrol;
    private int currentPatrolIndex = 0;

    public EnemyFollow(bool stopPatrol)
    {
        this.stopPatrol = stopPatrol;
    }

    void Start()
    {
        // 找到標籤為"Player"的物件
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        stopPatrol = true;
    }

    void Update()
    {
        // 計算敵人和主角之間的距離
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 如果敵人在跟隨範圍內，則跟隨主角
        if (distanceToPlayer < followRange)
        {
            // 停止巡邏
            stopPatrol = true;

            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Vector3 rightDirection = Quaternion.AngleAxis(90, Vector3.up) * direction;

            // 計算敵人需要轉向的角度
            Quaternion targetRotation = Quaternion.LookRotation(rightDirection, Vector3.up);

            // 漸進式地旋轉敵人
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 設置敵人的移動速度和方向
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // 繼續巡邏
            stopPatrol = false;

            // 如果有巡邏點，就朝著巡邏點移動
            if (patrolPoints.Count > 0)
            {
                // 計算與目前巡邏點之間的距離
                float distanceToPatrolPoint = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);

                // 如果到達了巡邏點，則前往下一個巡邏點
                if (distanceToPatrolPoint < 0.1f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                }

                Vector3 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
                Vector3 rightDirection = Quaternion.AngleAxis(90, Vector3.up) * direction;

                // 計算敵人需要轉向的角度
                Quaternion targetRotation = Quaternion.LookRotation(rightDirection, Vector3.up);

                // 漸進式地旋轉敵人
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // 設置敵人的移動速度和方向
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    private void OnEnable()
    {
        // 啟動時停止巡邏
        stopPatrol = true;
    }

    private void OnDisable()
    {
        // 結束時恢復巡邏
        stopPatrol = false;
    }
}

