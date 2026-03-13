using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public List<GameObject> weapons;
    public int activeWeaponIndex = 0;
    public Text ammoText;
    public GameObject[] m_WeaponImageGroup;

    private void Start()
    {
        ActivateWeapon(activeWeaponIndex);
        UpdateAmmoUI();
        m_WeaponImageGroup[0].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            WeaponPickup newWeapon = other.GetComponent<WeaponPickup>();

            if (newWeapon != null)
            {
                weapons.Add(newWeapon.gameObject);

                if (weapons.Count == 1)
                {
                    activeWeaponIndex = 0;
                    ActivateWeapon(activeWeaponIndex);
                    UpdateAmmoUI();
                }

                newWeapon.gameObject.SetActive(false);
            }
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        if (weapon != null)
        {
            weapons.Add(weapon);
            weapon.SetActive(false);

            int weaponCount = weapons.Count;

            if (weaponCount == 1)
            {
                ActivateWeapon(0);
            }

            Transform weaponParent = transform.GetChild(0).transform;
            Vector3 weaponPosition;
            Quaternion weaponRotation;

            if (weaponCount == 2)
            {
                weaponPosition = new Vector3(0.6360937f, -0.8100581f, 0.6458486f);
                weaponRotation = Quaternion.Euler(-0.282f, -93.949f, -0.104f);
            }
            else if (weaponCount == 3)
            {
                weaponPosition = new Vector3(0.638f, -0.63f, 1.063f);
                weaponRotation = Quaternion.Euler(2.793f, 168.599f, 178.418f);
            }
            else
            {
                weaponPosition = Vector3.zero;
                weaponRotation = Quaternion.identity;
            }

            weapon.transform.SetParent(weaponParent);
            weapon.transform.localPosition = weaponPosition;
            weapon.transform.localRotation = weaponRotation;
        }
    }

    public void Update()
    {
        if (weapons[activeWeaponIndex].GetComponent<Shoot>().isReloading)
        {
            return;
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (activeWeaponIndex == i)
                    return;

                weapons[activeWeaponIndex].SetActive(false);
                activeWeaponIndex = i;
                ActivateWeapon(activeWeaponIndex);

                for (int j = 0; j < m_WeaponImageGroup.Length; j++)
                {
                    m_WeaponImageGroup[j].SetActive(j == activeWeaponIndex);
                }

                UpdateAmmoUI();
            }
        }
    }

    void ActivateWeapon(int index)
    {
        weapons[index].SetActive(true);
    }

    void UpdateAmmoUI()
    {
        if (weapons[activeWeaponIndex] == null)
        {
            Debug.LogWarning("Weapon is null in WeaponSwitching.UpdateAmmoUI()");
            return;
        }

        Shoot currentWeapon = weapons[activeWeaponIndex].GetComponent<Shoot>();

        if (ammoText != null)
        {
            ammoText.text = currentWeapon.currentAmmo.ToString() + "/" + currentWeapon.maxAmmo.ToString() + "\n" + currentWeapon.currentTotalAmmo.ToString() + "/" + currentWeapon.maxTotalAmmo.ToString();
        }
        else
        {
            Debug.LogWarning("ammoText is null in WeaponSwitching.UpdateAmmoUI()");
        }
    }
}
