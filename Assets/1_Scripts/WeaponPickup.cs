using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPickup: MonoBehaviour
{
    public GameObject weaponPrefab;
    public TMP_Text pickupText; // 引用UI文本元素
    private bool canPickup = false;

    private void Start()
    {
        pickupText.gameObject.SetActive(false); // 初始时隐藏UI文本
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            pickupText.text = "按 E 拾取武器"; // 设置UI文本
            pickupText.gameObject.SetActive(true); // 显示UI文本
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            pickupText.gameObject.SetActive(false); // 隐藏UI文本
        }
    }

    private void PickupWeapon()
    {
        // 在这里添加将武器添加到玩家的逻辑，您可以参考原始 WeaponPickup 脚本
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        WeaponSwitching weaponSwitching = player.GetComponent<WeaponSwitching>();
        if (weaponSwitching != null)
        {
            weaponSwitching.AddWeapon(weaponPrefab);
        }
        gameObject.SetActive(false); // 隐藏拾取区域
        pickupText.gameObject.SetActive(false); // 隐藏UI文本
    }
}
