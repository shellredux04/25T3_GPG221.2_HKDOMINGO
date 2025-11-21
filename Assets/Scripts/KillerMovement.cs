using UnityEngine;

public class KillerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float rotateSpeed = 5f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    [Header("Targets")]
    public Transform player;
    public Transform hideSpot;
    [HideInInspector] public Vector3 soundPos;

    private void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Called from Patrol state
    public void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        Transform target = patrolPoints[patrolIndex];
        MoveTowards(target.position);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    // Called from Chase state
    public void ChasePlayer()
    {
        if (player == null) return;
        MoveTowards(player.position);
    }

    // Called from InvestigateSound state
    public void Investigate()
    {
        MoveTowards(soundPos);
    }

    // Called from Hide state
    public void Hide()
    {
        if (hideSpot == null) return;
        MoveTowards(hideSpot.position);
    }

    private void MoveTowards(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRot,
                rotateSpeed * Time.deltaTime
            );
        }
    }

    // Debug gizmos: patrol path + current targets
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] == null) continue;
                Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);

                int next = (i + 1) % patrolPoints.Length;
                if (patrolPoints[next] != null)
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[next].position);
            }
        }

        // Line to player
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }

        // Line to sound position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(soundPos, 0.2f);

        // Hide spot
        if (hideSpot != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(hideSpot.position, Vector3.one * 0.5f);
        }
    }
}
