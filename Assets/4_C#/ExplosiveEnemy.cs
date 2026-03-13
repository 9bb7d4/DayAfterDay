using UnityEngine;

public class ExplosiveEnemy : MonoBehaviour
{
    public float explosionRadius = 5.0f; // 爆炸半徑
    public float explosionForce = 100.0f; // 爆炸力度
    public int attackDamage = 10; // 攻擊造成的傷害

    private bool hasExploded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>(); // 獲取碰撞對象的玩家腳本
            if (player != null) // 如果碰撞對象是玩家
            {
                player.TakeDamage(attackDamage); // 減少玩家的生命值
            }

            // 當怪物碰到玩家時觸發爆炸
            Explode();
        }
    }

    private void Explode()
    {
        // 在這裡加入爆炸效果的程式碼，例如播放爆炸動畫或特效

        // 觸發物件消失
        
        Destroy(gameObject);
    }
}
