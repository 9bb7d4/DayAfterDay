using System.Collections;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController bossAnimatorController;
    public RuntimeAnimatorController bossWalkAnimatorController;

    public GameObject shockwavePrefab;
    public Transform shockwaveSpawnPoint;
    public float jumpForce = 10f;
    public float shockwaveRadius = 5f;
    public float shockwaveDuration = 1f;
    public int attackDamage = 10; // 新增攻擊傷害值

    private AnimatorOverrideController originalController;
    private bool isSkillCooldown = false; // 標記技能是否在冷卻中

    public float skillCooldownTime = 5f; // 觸發技能的冷卻時間

    void Start()
    {
        animator = GetComponent<Animator>();
        originalController = animator.runtimeAnimatorController as AnimatorOverrideController;

        // 遊戲一開始就觸發走路的 AnimatorController
        animator.runtimeAnimatorController = bossWalkAnimatorController;

        // 開始定期觸發技能
        InvokeRepeating("PerformSkill", skillCooldownTime, skillCooldownTime);
    }

    void PerformSkill()
    {
        // 檢查是否在冷卻中
        if (!isSkillCooldown)
        {
            StartCoroutine(TriggerSkill());
        }
    }

    IEnumerator TriggerSkill()
    {
        // 開始冷卻
        isSkillCooldown = true;

        // 切換到技能的 AnimatorController
        animator.runtimeAnimatorController = bossAnimatorController;

        // 觸發跳躍動畫
        animator.SetTrigger("Jump");

        yield return new WaitForSeconds(1.2f); // 假設跳躍動畫的持續時間為 1.2 秒

        // 創建衝擊波
        CreateShockwave();

        // 等待1.2秒，技能動畫結束

        // 切換回走路的 AnimatorController
        animator.runtimeAnimatorController = bossWalkAnimatorController;

        // 結束冷卻
        isSkillCooldown = false;
    }

    void CreateShockwave()
    {
        // 在指定位置生成衝擊波
        GameObject shockwave = Instantiate(shockwavePrefab, shockwaveSpawnPoint.position, Quaternion.identity);

        // 取得 ShockwaveScript 腳本
        ShockwaveScript shockwaveScript = shockwave.GetComponent<ShockwaveScript>();
        if (shockwaveScript != null)
        {
            // 設定初始旋轉
            Quaternion initialRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            // 設定衝擊波的參數
            shockwaveScript.SetParameters(shockwaveRadius, shockwaveDuration, initialRotation);

            // 設定傷害值
            shockwaveScript.SetDamage(attackDamage);
        }
    }
}
