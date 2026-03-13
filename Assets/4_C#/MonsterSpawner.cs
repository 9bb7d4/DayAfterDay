using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 怪物的預置體
    public Transform spawnPoint; // 生成怪物的位置
    public float spawnInterval = 5.0f; // 生成怪物的間隔時間（秒）
    public float spawnDuration = 60.0f; // 生成怪物的總持續時間（秒）

    private bool isSpawning = true; // 用於控制是否生成怪物

    private void Start()
    {
        // 啟動生成怪物的協程
        StartCoroutine(SpawnMonsters());

        // 在指定的時間後停止生成怪物
        Invoke("StopSpawning", spawnDuration);
    }

    private IEnumerator SpawnMonsters()
    {
        while (isSpawning)
        {
            // 等待指定的間隔時間
            yield return new WaitForSeconds(spawnInterval);

            // 生成怪物在指定位置
            Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void StopSpawning()
    {
        isSpawning = false;
    }
}
