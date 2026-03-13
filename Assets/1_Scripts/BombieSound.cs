using UnityEngine;

public class BombieSound : MonoBehaviour
{
    public AudioClip bombieSound;  // 要播放的音效
    private AudioSource audioSource;

    private void Start()
    {
        // 在同一物件上添加 AudioSource，如果沒有的話
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 設置 AudioSource 的音效
        audioSource.clip = bombieSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰到的物件是否標有 "Bombie" 標籤
        if (other.CompareTag("Bombie"))
        {
            // 播放音效
            if (audioSource != null && bombieSound != null)
            {
                audioSource.PlayOneShot(bombieSound);
            }
        }
    }
}
