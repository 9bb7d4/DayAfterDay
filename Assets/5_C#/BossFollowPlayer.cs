// BossFollowPlayer.cs
using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    public float speed = 5f; // Boss 的移動速度
    public float stoppingDistance = 2f; // Boss 與玩家的停止距離
    private Transform playerTransform; // 玩家的 Transform
    private BossSkill2 bossSkill2; // 參考 BossSkill2 腳本

    void Start()
    {
        // 獲取玩家的 Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 獲取 BossSkill2 腳本
        bossSkill2 = GetComponent<BossSkill2>();
    }

    void Update()
    {
        // 如果碰到玩家，停止跟隨
        if (IsPlayerInRange())
        {
            // 在此可以添加停止跟隨時的行為

            // 如果 Boss 正在衝刺，停止衝刺
            if (bossSkill2.IsDashing())
            {
                bossSkill2.StopDash();
                bossSkill2.OnDashEnd();
            }

            // 設定其他行為正在進行
            bossSkill2.SetOtherActionInProgress(true);

            // 停止追蹤
            bossSkill2.SetOtherActionInProgress(false);
        }
        else if (bossSkill2.IsFollowing())
        {
            // 如果玩家不在範圍內，且 Boss 沒有在衝刺和沒有其他行為正在進行，繼續跟隨玩家
            FollowPlayer();

            // 設定其他行為已結束
            bossSkill2.SetOtherActionInProgress(false);
        }
    }

    bool IsPlayerInRange()
    {
        // 計算 Boss 與玩家的距離
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 判斷是否在停止距離內
        return distanceToPlayer <= stoppingDistance;
    }

    void FollowPlayer()
    {
        // 計算 Boss 到玩家的向量
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // 將方向向量標準化，使得其長度為 1
        directionToPlayer.Normalize();

        // 使用向量運算移動 Boss
        transform.Translate(directionToPlayer * speed * Time.deltaTime, Space.World);

        // 設定 Boss 的 X 軸朝向玩家
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

        // 手動調整 Y 軸旋轉為 90 度
        transform.rotation *= Quaternion.Euler(0, 90, 0);
    }

}
