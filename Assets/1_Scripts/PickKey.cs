using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickKey : MonoBehaviour
{
    int keyCount = 0;
    public GameObject propsWall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("keyCount");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            keyCount += 1;

            Destroy(propsWall);
        }
    }
}