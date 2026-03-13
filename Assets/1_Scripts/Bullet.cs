using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public static Bullet instance; // 靜態變數
    public int damage = 1; // 子彈攻擊力

    // 定義公共靜態變數 actualDamage，用於傳遞傷害值
    public static int actualDamage = 0;
    private void Awake()
    {
        instance = this; // 初始化靜態變數
    }

    public void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>(); // 獲取碰撞對象的敵人腳本
        if (enemy != null) // 如果碰撞對象是敵人
        {
            // 計算實際傷害值並賦值給 actualDamage 變數
            actualDamage = Player.instance.damageLevel * damage;
            enemy.TakeDamage(actualDamage); // 減少敵人的生命值
        }
        Destroy(gameObject); // 銷毀子彈物體
    }


    private void Start()
    {

        // 設定子彈存在的時間，超過時間就銷毀
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 子彈碰到任何東西都要銷毀
        Destroy(gameObject);
    }
}
