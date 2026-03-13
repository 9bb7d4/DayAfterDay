using UnityEngine;

public class watergate : MonoBehaviour
{
    public float loweredHeight = -1.0f;
    public float loweringDuration = 300.0f; // 将持续时间改为 300 秒

    private Vector3 initialPosition;
    private float elapsedTime = 0.0f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (elapsedTime < loweringDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / loweringDuration;
            Vector3 targetPosition = new Vector3(initialPosition.x, loweredHeight, initialPosition.z);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
        }
    }
}
