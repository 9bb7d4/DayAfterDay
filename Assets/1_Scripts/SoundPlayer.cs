using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClip;

    void Start()
    {
        // 每4秒钟调用一次 PlaySound 方法
        InvokeRepeating("PlaySound",0.0f ,4.0f);
    }

    void PlaySound()
    {
        // 播放声音
        AudioSource.PlayClipAtPoint(soundClip, transform.position);
        // 在控制台中输出一条消息
        //Debug.Log("Sound played!");
    }
}
