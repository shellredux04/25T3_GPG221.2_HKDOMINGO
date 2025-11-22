using UnityEngine;

public class CivilianMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 6f;

    public Transform[] patrolPoints;
    int index = 0;

    public Transform player;

    void Awake()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    // Patrol movement
    public void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[index];
        MoveTowards(target.position, walkSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.4f)
            index = (index + 1) % patrolPoints.Length;
    }

    // Idle - do nothing
    public void Idle()
    {
        // Could add animations here
    }

    // Run from player
    public void RunAway()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        Vector3 target = transform.position + dir * 4f;
        MoveTowards(target, runSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, 6f * Time.deltaTime);
        }
    }
}
