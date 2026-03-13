using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class BossDisappear : MonoBehaviour
{
    public GameObject replacementObjectPrefab; // 新物件的預製體
    public Transform spawnPoint; // 新物件生成的位置
    public RuntimeAnimatorController firstAnimationController; // 第一個動畫的控制器
    public RuntimeAnimatorController secondAnimationController; // 第二個動畫的控制器

    public bool bossReplaced = false;

    void Update()
    {
        if (!bossReplaced)
        {
            // 尋找具有 "Boss" 標籤的物件
            GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");

            // 檢查 boss 物件是否存在且已經消失
            if (bossObject == null || !bossObject.activeSelf)
            {
                bossReplaced = true;
                StartCoroutine(ReplaceBoss());
            }
           
        }
        
    }

    IEnumerator ReplaceBoss()
    {
        // 生成新物件
        GameObject replacementObject = Instantiate(replacementObjectPrefab, spawnPoint.position, Quaternion.identity);

        // 獲取新物件上的 Animator
        Animator animator = replacementObject.GetComponent<Animator>();

        if (animator != null)
        {
            // 撥放第一個動畫
            animator.runtimeAnimatorController = firstAnimationController;

            // 等待第一個動畫撥放完畢
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // 切換到第二個動畫
            animator.runtimeAnimatorController = secondAnimationController;

            // 循環撥放第二個動畫
            while (true)
            {
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
                animator.Play(0, 0, 0); // 重播第二個動畫
            }
        }
    }
}
