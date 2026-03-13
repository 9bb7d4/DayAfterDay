using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossSkill3 : MonoBehaviour
{
    public GameObject enemyPrefab; // 要生成的怪物
    public Transform[] spawnPoints; // 生成怪物的位置陣列
    public float spawnInterval = 5f; // 生成怪物的間隔
    public float bossHealthThreshold = 50f; // Boss血量的閾值

    public TMP_Text spawnMessage; // 顯示生成提示的UI文本
    public float spawnMessageDuration = 3f; // 生成提示的顯示時間

    public RuntimeAnimatorController bossAnimationController; // 使用的Animator控制器
    public RuntimeAnimatorController bossWalkAnimatorController; // 走路動畫的Animator控制器

    private bool isSkillActive = false; // 表示技能是否已啟動
    private float nextSpawnTime = 0f; // 下一次生成怪物的時間
    private AnimatorOverrideController overrideController; // 用來覆蓋現有動畫控制器的覆蓋控制器

    void Start()
    {
        // 隱藏生成提示文本
        spawnMessage.gameObject.SetActive(false);

        // 創建覆蓋控制器
        overrideController = new AnimatorOverrideController(bossWalkAnimatorController);
        GetComponent<Animator>().runtimeAnimatorController = overrideController;
    }

    void Update()
    {
        // 檢查Boss的血量是否低於50%
        if (!isSkillActive && GetBossHealthPercentage() <= bossHealthThreshold)
        {
            // 啟動Boss技能3
            isSkillActive = true;
            // 播放技能動畫
            if (overrideController != null)
            {
                StopOtherAnimations();
                foreach (AnimationClip clip in bossWalkAnimatorController.animationClips)
                {
                    overrideController[clip.name] = clip;
                }
                GetComponent<Animator>().SetTrigger("Skill3Trigger");
            }
        }

        // 如果Boss技能3已啟動，且到達生成怪物的時間
        if (isSkillActive && Time.time >= nextSpawnTime)
        {
            // 生成怪物
            StartCoroutine(SpawnEnemyWithMessage());

            // 設定下一次生成怪物的時間
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void StopOtherAnimations()
    {
        // 停止所有其他動畫
        foreach (AnimationClip clip in bossWalkAnimatorController.animationClips)
        {
            overrideController[clip.name] = clip;
        }
    }

    float GetBossHealthPercentage()
    {
        // 在這裡獲取Boss的血量百分比
        // 這取決於你的遊戲結構和Boss的血量系統
        // 以下僅為示例，請替換為實際代碼
        float currentHealth = GetComponent<Enemy>().currentHealth;
        float maxHealth = GetComponent<Enemy>().maxHealth;

        // 防止除以零
        if (maxHealth > 0)
        {
            return (currentHealth / maxHealth) * 100f;
        }
        else
        {
            return 100f; // 如果最大血量為零，假定Boss的血量為100%
        }
    }

    IEnumerator SpawnEnemyWithMessage()
    {
        // 顯示生成提示
        spawnMessage.gameObject.SetActive(true);

        // 在文字顯示的同時，切換到技能動畫
        if (overrideController != null)
        {
            GetComponent<Animator>().runtimeAnimatorController = overrideController;
        }

        yield return new WaitForSeconds(spawnMessageDuration);

        // 隱藏生成提示
        spawnMessage.gameObject.SetActive(false);

        // 切換回走路動畫
        if (bossWalkAnimatorController != null)
        {
            GetComponent<Animator>().runtimeAnimatorController = bossWalkAnimatorController;
        }

        // 在隨機的生成點生成怪物
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}
