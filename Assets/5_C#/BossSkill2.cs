using System.Collections;
using UnityEngine;

public class BossSkill2 : MonoBehaviour
{
    public float dashSpeed = 10f; // 衝刺速度
    public float dashDuration = 1f; // 衝刺持續時間
    public float cooldownTime = 15f; // 技能冷卻時間
    public RuntimeAnimatorController dashAnimatorController; // 衝刺時的Animator Controller
    public RuntimeAnimatorController walkAnimatorController; // 走路動畫的Animator Controller

    private bool isDashing = false; // 標記是否正在衝刺
    private bool isOtherActionInProgress = false; // 標記是否有其他行為正在進行
    private Transform playerTransform; // 玩家的Transform
    private Rigidbody bossRigidbody; // Boss的Rigidbody
    private bool isFollowing = true; // 控制是否追蹤玩家
    private Animator bossAnimator; // Boss的Animator組件
    private float lastTriggerTime = 0f; // 上一次觸發的時間

    void Start()
    {
        // 獲取玩家的Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 獲取Boss的Rigidbody
        bossRigidbody = GetComponent<Rigidbody>();

        // 獲取Boss的Animator組件
        bossAnimator = GetComponent<Animator>();

        // 設定初始的Animator Controller為走路動畫
        if (walkAnimatorController != null)
        {
            bossAnimator.runtimeAnimatorController = walkAnimatorController;
        }

        // 初始化上一次觸發的時間
        lastTriggerTime = Time.time;
    }

    void Update()
    {
        // 檢查是否在指定的冷卻時間內且不在衝刺中且沒有其他行為正在進行
        if (Time.time - lastTriggerTime >= cooldownTime && !isDashing && !isOtherActionInProgress)
        {
            // 切換Animator Controller
            bossAnimator.runtimeAnimatorController = dashAnimatorController;

            // 播放衝刺動畫
            bossAnimator.SetTrigger("DashTrigger");

            // 停止追蹤
            isFollowing = false;

            // 開始衝刺技能
            StartCoroutine(Dash());

            // 設定上一次觸發的時間
            lastTriggerTime = Time.time;
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void StopDash()
    {
        // 在這裡可以添加停止衝刺時的行為

        // 停止衝刺
        isDashing = false;
    }

    public void SetOtherActionInProgress(bool value)
    {
        isOtherActionInProgress = value;
    }

    public bool IsOtherActionInProgress()
    {
        return isOtherActionInProgress;
    }

    IEnumerator Dash()
    {
        // 開始冷卻
        isDashing = true;

        // 設定其他行為正在進行
        isOtherActionInProgress = true;

        // 計算衝刺的方向
        Vector3 dashDirection = (playerTransform.position - transform.position).normalized;

        // 停止Boss的物理運動
        bossRigidbody.velocity = Vector3.zero;

        // 使用 AddForce 模擬衝刺，確保 Collision Detection 模式設定為 Continuous
        bossRigidbody.AddForce(new Vector3(dashDirection.x, 0f, dashDirection.z) * dashSpeed, ForceMode.VelocityChange);

        // 等待衝刺持續時間
        yield return new WaitForSeconds(dashDuration);

        // 結束冷卻
        isDashing = false;

        // 表示其他行為已經結束
        isOtherActionInProgress = false;

        // 開始追蹤
        isFollowing = true;

        // 還原Animator Controller為走路動畫
        if (walkAnimatorController != null)
        {
            bossAnimator.runtimeAnimatorController = walkAnimatorController;
        }

        // 在衝刺結束時的動作
        OnDashEnd();

        // 檢查是否有觸發器碰撞（假設觸發器的 Tag 為 "Obstacle"）
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f, LayerMask.GetMask("Obstacle"));
        if (colliders.Length > 0)
        {
            // 跌倒的邏輯
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {
        // 設定 Boss 的跌倒動畫
        bossAnimator.SetTrigger("FallDownTrigger");

        // 停止追蹤
        isFollowing = false;

        // 等待跌倒的持續時間
        yield return new WaitForSeconds(2.0f);

        // 還原 Animator Controller 為走路動畫
        if (walkAnimatorController != null)
        {
            bossAnimator.runtimeAnimatorController = walkAnimatorController;
        }

        // 在跌倒結束時的動作
        OnFallDownEnd();
    }

    public void OnDashEnd()
    {
        // 在這裡可以添加停止衝刺時的行為
        // 例如，設定其他行為已結束
        isOtherActionInProgress = false;
    }

    public void OnFallDownEnd()
    {
        // 在這裡可以添加跌倒結束時的行為
        // 例如，設定其他行為已結束
        isOtherActionInProgress = false;
    }

    public bool IsFollowing()
    {
        return isFollowing;
    }
}
