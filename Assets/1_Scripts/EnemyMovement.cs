using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform[] waypoints;
    public float followRange = 10f;
    public float rotationSpeed = 5f;

    private Transform playerTransform;
    private int currentWaypointIndex = 0;
    private bool isFollowing = true;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isFollowing == true)
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Move towards the current waypoint
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (currentWaypoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation *= Quaternion.Euler(0f, 0f, 0f); // Set target rotation to (90, 150, 60) degrees
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Check if the enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            // Switch to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Check if the player is within follow range
        if (Vector3.Distance(transform.position, playerTransform.position) < followRange)
        {
            // Start following the player
            isFollowing = true;
        }
    }

    private void FollowPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Calculate the target rotation to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation *= Quaternion.Euler(0f, 0f, 0f); // Set target rotation to (90, 150, 60) degrees
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move towards the player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Check if the player is no longer within follow range
        if (Vector3.Distance(transform.position, playerTransform.position) > followRange)
        {
            // Stop following the player and resume patrolling
            isFollowing = false;
        }
    }
}
