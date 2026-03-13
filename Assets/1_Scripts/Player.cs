using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int damageLevel = 1;
    public static Player instance;

    public Text maxHealthText;
    public float bulletDamage = 10f;
    public float bulletDamageIncrease = 5f;
    public GameObject levelUpOptionsUI;
    public int maxHealth = 100;
    public int currentHealth;
    public int currentLevel = 1;
    public int currentExp = 0;

    private int _expToNextLevel = 100; // 改為私有變數

    public int ExpToNextLevel
    {
        get { return _expToNextLevel; }
        set
        {
            _expToNextLevel = value;
            UpdateExpToNextLevelUI();
            expBar.SetMaxExp(_expToNextLevel);
        }
    }

    public int attackDamage = 10;
    public int maxHealthIncreasePerLevel = 10;
    public int attackDamageIncreasePerLevel = 5;

    public HealthBar healthBar;
    public EXPBAR expBar;
    public Text levelText;
    public Text expText;
    public Text expToNextLevelText;
    public GameObject gameOverUI;

    public AudioSource m_explodeSource;
    public AudioClip m_explode;
    private void UpdateMaxHealthUI()
    {
        maxHealthText.text = "Max Health: " + maxHealth.ToString();
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        instance = this;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        expBar.SetMaxExp(_expToNextLevel);
        UpdateLevelUI();
        UpdateMaxHealthUI();

        m_explodeSource= GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                TakeDamage(enemy.damage);
                if (enemy.currentHealth <= 0)
                {
                    AddExp(enemy.giveExp());
                }
            }
        }
        if (collision.gameObject.tag == "Bombie")
        {
            m_explodeSource.PlayOneShot(m_explode, 1f);
            Debug.Log("Bomb");
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            EndGame();
        }
    }

    public bool IsHealthFull()
    {
        return currentHealth == maxHealth;
    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Heal(int healthAmount)
    {
        currentHealth += healthAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void AddExp(int expAmount)
    {
        currentExp += expAmount;
        UpdateExpUI();
        while (currentExp >= ExpToNextLevel) // 使用 ExpToNextLevel 屬性
        {
            levelUp();
            currentExp -= ExpToNextLevel; // 使用 ExpToNextLevel 屬性
            ExpToNextLevel += 50; // 使用 ExpToNextLevel 屬性
            ShowLevelUpOptionsUI();
            UpdateExpUI();
            UpdateExpToNextLevelUI();
            expBar.SetExp(currentExp);
        }
    }

    public void levelUp()
    {
        currentLevel++;
    }

    private void UpdateLevelUI()
    {
        levelText.text = "Level: " + currentLevel.ToString();
        UpdateMaxHealthUI();
    }

    private void UpdateExpUI()
    {
        expText.text = "EXP: " + currentExp.ToString();
    }

    private void UpdateExpToNextLevelUI()
    {
        expToNextLevelText.text = "EXP to Next Level: " + ExpToNextLevel.ToString();
    }

    public void ShowLevelUpOptionsUI()
    {
        levelUpOptionsUI.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void HideLevelUpOptionsUI()
    {
        levelUpOptionsUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetMouseCursorVisible(bool visible)
    {
        Cursor.visible = visible;
    }

    public void UpgradeMaxHealth()
    {
        maxHealth += maxHealthIncreasePerLevel;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UpdateLevelUI();
        HideLevelUpOptionsUI();
    }

    public void UpgradeAttackDamage()
    {
        damageLevel++;
        UpdateLevelUI();
        HideLevelUpOptionsUI();
    }

    void Update()
    {
        maxHealthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        expBar.SetExp(currentExp);
    }
}
