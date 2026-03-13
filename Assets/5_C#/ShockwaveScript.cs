// ShockwaveScript.cs
using System.Collections;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    private float radius;
    private float duration;
    private Quaternion initialRotation;
    private int damage; // 新增傷害值變數

    public void SetParameters(float shockwaveRadius, float shockwaveDuration, Quaternion initialRotation)
    {
        radius = shockwaveRadius;
        duration = shockwaveDuration;
        this.initialRotation = initialRotation;

        StartCoroutine(ExpandAndDestroy());
    }

    // 新增 SetDamage 方法
    public void SetDamage(int value)
    {
        damage = value;
    }

    IEnumerator ExpandAndDestroy()
    {
        float elapsedTime = 0f;

        // 取得初始模型的縮放
        Vector3 initialScale = transform.localScale;

        // 設定初始旋轉
        transform.rotation = initialRotation;

        while (elapsedTime < duration)
        {
            // 根據擴散的比例計算 scaleFactor
            float scaleFactor = Mathf.Lerp(0f, radius, elapsedTime / duration);

            // 調整縮放，同時保持 X 和 Y 軸的比例
            float newScaleX = initialScale.x + scaleFactor;
            float newScaleY = initialScale.y + scaleFactor;

            // 設定衝擊波的縮放
            transform.localScale = new Vector3(newScaleX, newScaleY, initialScale.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    // 修改碰撞偵測，只對標籤為 "Player" 的物件做處理
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // 衝擊波碰到玩家後，自毀
            Destroy(gameObject);
        }
    }
}
