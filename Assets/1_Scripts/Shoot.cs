using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public int maxAmmo = 30;
    public int currentAmmo;
    public int maxTotalAmmo = 90;
    public int currentTotalAmmo;
    public float reloadTime = 1f;
    public float fireRate = 0.1f;
    public Text ammoText;
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip shootSound;
    public Text LowAmmoText;
    private bool isLowAmmoWarningDisplayed = false;

    public bool isReloading = false;
    private bool isFiring = false;

    void Start()
    {
        ammoText = GameObject.Find("ammo").GetComponent<Text>();
        currentAmmo = maxAmmo;
        currentTotalAmmo = maxTotalAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (Input.GetMouseButton(0) && currentAmmo > 0 && !isReloading)
        {
            if (!isFiring)
            {
                isFiring = true;
                InvokeRepeating("Fire", 0f, fireRate);
            }
        }
        else
        {
            if (isFiring)
            {
                isFiring = false;
                CancelInvoke("Fire");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentTotalAmmo > 0 && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }
        if (currentAmmo < 5 && !isLowAmmoWarningDisplayed)
        {
            ShowLowAmmoWarning();
        }
        else if (currentAmmo >= 5 && isLowAmmoWarningDisplayed)
        {
            HideLowAmmoWarning();
        }
    }

    void ShowLowAmmoWarning()
    {
        isLowAmmoWarningDisplayed = true;

        if (LowAmmoText != null)
        {
            LowAmmoText.text = "Low Ammo !";
        }
        else
        {
            Debug.LogWarning("LowAmmoText is null.");
        }
    }

    void HideLowAmmoWarning()
    {
        isLowAmmoWarningDisplayed = false;

        if (LowAmmoText != null)
        {
            LowAmmoText.text = "";
        }
        else
        {
            Debug.LogWarning("LowAmmoText is null.");
        }
    }

    void Fire()
    {
        currentAmmo--;
        UpdateAmmoUI();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletSpawn.forward * bulletSpeed;

        audioSource.PlayOneShot(shootSound,0.5f);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound,0.5f);

        yield return new WaitForSeconds(reloadTime);

        int bulletToAdd = Mathf.Min(maxAmmo - currentAmmo, currentTotalAmmo);

        currentAmmo += bulletToAdd;
        currentTotalAmmo -= bulletToAdd;

        UpdateAmmoUI();

        isReloading = false;
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = " " + currentAmmo.ToString() + "/" + maxAmmo.ToString() + "\n " + currentTotalAmmo.ToString() + "/" + maxTotalAmmo.ToString();
        }
        else
        {
            Debug.LogWarning("ammoText is null in Shoot.UpdateAmmoUI()");
        }
    }

    public void AddAmmo(int ammoToAdd)
    {
        maxTotalAmmo += ammoToAdd;
        currentTotalAmmo += ammoToAdd;
        UpdateAmmoUI();
        Debug.Log("Ammo added! Current Total Ammo: " + currentTotalAmmo);
    }
}
