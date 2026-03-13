using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup2 : MonoBehaviour
{
    public GameObject weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 撿起武器
            PickupWeapon();
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

        // 隐藏拾取区域
        gameObject.SetActive(false);

        // 隐藏UI文本
        Text pickupText = GetComponentInChildren<Text>();
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false);
        }
    }
}
