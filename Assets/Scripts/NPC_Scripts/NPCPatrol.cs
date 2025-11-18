using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    private int currentPoint = 0;

    void Update()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }

        // Rotate toward target
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
            transform.forward = direction;
    }
}
