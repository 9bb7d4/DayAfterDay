using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackRange = 2f;  // 攻擊範圍
    public float attackDamage = 1f;  // 攻擊傷害
    public float attackInterval = 1f; // 攻擊間隔
    private float lastAttackTime; // 上次攻擊時間

    private Transform player;  // 玩家的Transform組件

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // 找到標記為"Player"的物體
    }

    void Update()
    {
        // 如果玩家在攻擊範圍內，就攻擊玩家
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // 如果超過攻擊間隔，就攻擊玩家
            if (Time.time - lastAttackTime >= attackInterval)
            {
                lastAttackTime = Time.time; // 記錄上次攻擊時間
                Attack();
            }
        }
    }

    void Attack()
    {
        // 在這裡加入攻擊動作，例如撥放攻擊動畫

        // 對玩家造成傷害
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage((int)attackDamage); // 修改此行，將浮點數的 damage 強制轉換為整數
        }
    }
}
