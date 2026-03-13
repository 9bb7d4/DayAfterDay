using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float attackRange = 10f;
    public float attackRate = 1f;
    public float projectileSpeed = 10f;
    public float attackCooldown = 2f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public AudioSource m_audioSource;
    public AudioClip shootSound;

    private Transform playerTransform;
    private float nextAttackTime = 0f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < attackRange)
        {
            if (Time.time > nextAttackTime)
            {
                // 判斷是否有障礙物
                RaycastHit hit;
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, direction, out hit, distanceToPlayer))
                {
                    if (hit.transform != playerTransform)
                    {
                        // 如果有障礙物，則不攻擊並更新攻擊冷卻時間
                        nextAttackTime = Time.time + attackCooldown;
                        return;
                    }
                }

                // 攻擊玩家
                Attack();

                // 設定下一次攻擊時間
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    void Attack()
    {
        // 建立投射物
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // 設定投射物的方向
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        // 設定投射物的速度和方向
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = direction * projectileSpeed;

        // 播放射擊音效
        m_audioSource.PlayOneShot(shootSound);
    }
}


