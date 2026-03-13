using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public GameObject enemyPrefab; // 要生成的怪物
    public Transform spawnPoint; // 生成怪物的位置
    public float spawnInterval = 5f; // 生成怪物的間隔
    public float bossHealthThreshold = 50f; // Boss血量的閾值

    private bool isSkillActive = false; // 表示技能是否已啟動
    private float nextSpawnTime = 0f; // 下一次生成怪物的時間

    public Slider healthSlider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;

    private int maxHealth = 100; // 假設 Boss 的最大生命值為 100，請根據實際情況調整
    private int currentHealth; // Boss 的當前生命值

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        fillImage.color = fullHealthColor;
    }

    void Update()
    {
        // 檢查Boss的血量是否低於50%
        if (!isSkillActive && GetBossHealthPercentage() <= bossHealthThreshold)
        {
            // 啟動Boss技能3
            isSkillActive = true;
        }

        // 如果Boss技能3已啟動，且到達生成怪物的時間
        if (isSkillActive && Time.time >= nextSpawnTime)
        {
            // 生成怪物
            SpawnEnemy();

            // 設定下一次生成怪物的時間
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    float GetBossHealthPercentage()
    {
        // 在這裡獲取Boss的血量百分比
        return (currentHealth / (float)maxHealth) * 100f;
    }

    void SpawnEnemy()
    {
        // 在生成點生成怪物
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 扣除生命值

        // 更新UI血量條
        UpdateHealthUI();

        if (currentHealth <= 0) // 生命值歸零
        {
            Die(); // 死亡
        }
        else
        {
            StartCoroutine(DamageEffect()); // 受傷效果
        }
    }

    private IEnumerator DamageEffect()
    {
        // 假設有 MeshRenderer
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = Color.red; // 受傷時顯示紅色
        }

        yield return new WaitForSeconds(0.1f); // 等待一段時間

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = Color.white; // 恢復原始顏色
        }
    }

    void Die()
    {
        // Boss 死亡的相應處理
        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.value = currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercentage);
    }
}
