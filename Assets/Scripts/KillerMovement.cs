using UnityEngine;

public class KillerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 5f;

    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    public Transform player;
    public Transform hideSpot;
    public Vector3 soundPos;

    private void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[patrolIndex];
        MoveTowards(target.position);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    public void ChasePlayer()
    {
        if (player == null) return;
        MoveTowards(player.position);
    }

    public void Investigate()
    {
        MoveTowards(soundPos);
    }

    public void Hide()
    {
        if (hideSpot == null) return;
        MoveTowards(hideSpot.position);
    }

    private void MoveTowards(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate toward movement direction
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotateSpeed * Time.deltaTime);
        }
    }
}
