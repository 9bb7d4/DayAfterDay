using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject dropItemPrefab; // 控制掉落物品的遊戲物件
    public int dropItemCount = 1; // 控制掉落物品的數量
    public float dropItemProbability = 0.5f; // 控制掉落物品的機率

    public int level = 1; // 怪物等級
    public float moveSpeed = 3f; // 怪物移動速度
    public int maxHealth = 10; // 怪物最大生命值
    public int currentHealth; // 怪物當前生命值
    public int damage = 10; // 怪物攻擊傷害
    public int expValue = 10;
    public Material damageMaterial; // 受傷時顯示的材質
    public GameObject objectToHide; // 要隱藏的物件

    private Dictionary<MeshRenderer, Material> originalMaterials = new Dictionary<MeshRenderer, Material>(); // 原始的材質

    // 新增UI血量條相關變數
    public Slider healthSlider;
    public Image fillImage;
    public Color fullHealthColor = Color.red;
    public Color zeroHealthColor = Color.red;

    void Update()
    {
        // 檢查血量是否低於50
        if (currentHealth <= 100)
        {
            // 血量低於50，隱藏物件
            objectToHide.SetActive(false);
        }
        else
        {
            // 血量大於等於50，顯示物件
            objectToHide.SetActive(true);
        }

        // 更新UI血量條
        UpdateHealthUI();
    }

    public int giveExp()
    {
        return expValue;
    }

    private void Start()
    {
        currentHealth = maxHealth; // 初始化當前生命值
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(); // 取得所有子物體的 MeshRenderer

        foreach (MeshRenderer renderer in renderers)
        {
            originalMaterials.Add(renderer, renderer.material); // 保存原始材質
        }

        // 初始化UI血量條
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        fillImage.color = fullHealthColor;
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
        foreach (MeshRenderer renderer in originalMaterials.Keys)
        {
            renderer.material = damageMaterial; // 切換為受傷材質
        }

        yield return new WaitForSeconds(0.1f); // 等待一段時間

        foreach (MeshRenderer renderer in originalMaterials.Keys)
        {
            renderer.material = originalMaterials[renderer]; // 切換回原始材質
        }
    }

    private void Die()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= dropItemProbability)
        {
            // 生成掉落物品
            for (int i = 0; i < dropItemCount; i++)
            {
                GameObject dropItem = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
                dropItem.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                // 其他控制掉落物品的程式碼
                // ...
            }
        }
        int exp = expValue * level;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // 找到玩家物件
        Player player = playerObj.GetComponent<Player>(); // 取得玩家腳本
        player.AddExp(exp); // 增加玩家的經驗值
        Destroy(gameObject); // 銷毀怪物物件
    }

    // 更新UI血量條的函數
    void UpdateHealthUI()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.value = currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercentage);
    }
}
