using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }


    void Update()
    {
        if (text != null)
        {
            int actualDamage = Player.instance.damageLevel * Bullet.instance.damage;
            text.text = "BulletDamage : " + actualDamage.ToString();
        }
    }
}
