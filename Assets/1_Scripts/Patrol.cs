using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 5f;
    public float detectionRadius = 10f;
    public float maxMoveTime = 5f;

    private Transform currentPatrolPoint;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private Transform target;
    private float currentMoveTime = 0f;

    void Start()
    {
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    void Update()
    {
        if (!isChasing)
        {
            // Move towards the current patrol point
            transform.position = Vector3.MoveTowards(transform.position, currentPatrolPoint.position, moveSpeed * Time.deltaTime);

            // If we reach the patrol point, switch to the next one
            if (transform.position == currentPatrolPoint.position)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                currentPatrolPoint = patrolPoints[currentPatrolIndex];
            }

            // Check for nearby targets
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    target = collider.transform;
                    isChasing = true;
                    currentMoveTime = 0f;
                    break;
                }
            }
        }
        else
        {
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // If we reach the target, stop chasing and return to patrol
            if (transform.position == target.position)
            {
                isChasing = false;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                currentPatrolPoint = patrolPoints[currentPatrolIndex];
            }

            // If we have been chasing for too long, return to patrol
            currentMoveTime += Time.deltaTime;
            if (currentMoveTime >= maxMoveTime)
            {
                isChasing = false;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                currentPatrolPoint = patrolPoints[currentPatrolIndex];
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
