using UnityEngine;
using System.Collections;

public class WeaponRecoil : MonoBehaviour
{
    //槍枝後座力
    public float recoilForce = 2.0f;
    public float recoilDuration = 0.2f;


    private bool isRecoiling = false;

    // 省略其他變數...

    void  Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ApplyRecoil();
        }

        // ...
    }

    void ApplyRecoil()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;
            Vector3 recoilDirection = -transform.forward;
            StartCoroutine(Recoil(recoilDirection));
        }
    }

    IEnumerator Recoil(Vector3 recoilDirection)
    {
        float elapsedTime = 0f;

        while (elapsedTime < recoilDuration)
        {
            float recoilAmount = Mathf.Lerp(recoilForce, 0f, elapsedTime / recoilDuration);
            transform.position += recoilDirection * recoilAmount * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRecoiling = false;
    }

    // ...
}
