using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoToAdd = 10;

    private void Update()
    {
        transform.Rotate(Vector3.up, 60.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WeaponSwitching weaponSwitching = other.gameObject.GetComponentInChildren<WeaponSwitching>();
            if (weaponSwitching != null)
            {
                foreach (var weapon in weaponSwitching.weapons)
                {
                    Shoot shootScript = weapon.GetComponent<Shoot>();
                    if (shootScript != null)
                    {
                        int maxTotalAmmo = shootScript.maxTotalAmmo;
                        int currentTotalAmmo = shootScript.currentTotalAmmo;
                        int newTotalAmmo = currentTotalAmmo + ammoToAdd;

                        if (newTotalAmmo > maxTotalAmmo)
                        {
                            newTotalAmmo = maxTotalAmmo;
                        }

                        int ammoToAddLimited = newTotalAmmo - currentTotalAmmo;
                        if (ammoToAddLimited > 0)
                        {
                            shootScript.currentTotalAmmo += ammoToAddLimited;
                            shootScript.UpdateAmmoUI();
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
}
