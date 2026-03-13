using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject EnemyPre;
    
    public int Num = 3;
    private float timer;
    
    void Update()
    {
        //计时器时间增加
        timer += Time.deltaTime;
        //2s检测一次
        if (timer > 2)
        {
            //重置计时器
            timer = 0;
            //查看有几个敌人
            int n = transform.childCount;
            if (n < Num)
            {
                Vector3 v = transform.position;
                v.x += Random.Range(-8, 8);
                v.y += Random.Range(-2, 1);
                v.z += Random.Range(-8, 8);
                //随机确定一个旋转
                Quaternion q = Quaternion.Euler(0, Random.Range(0, 360), 0);
                //创建一个敌人
                GameObject enemy = GameObject.Instantiate(EnemyPre, v, q);
                //设置父子关系
                enemy.transform.SetParent(transform);
            }
        }
    }
}
