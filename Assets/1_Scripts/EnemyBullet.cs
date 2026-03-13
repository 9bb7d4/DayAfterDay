using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1; // 子彈攻擊力
    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>(); // 獲取碰撞對象的玩家腳本
        if (player != null) // 如果碰撞對象是玩家
        {
            player.TakeDamage(damage); // 減少玩家的生命值
        }
        Destroy(gameObject); // 銷毀子彈物體
    }

    private void Start()
    {
        // 設定子彈存在的時間，超過時間就銷毀
        Destroy(gameObject, 2f);
    }

   
}
