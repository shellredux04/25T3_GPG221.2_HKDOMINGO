using UnityEngine;

public class NPCDetectPlayer : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 7f;
    public float chaseSpeed = 3.5f;
    public bool chasePlayer = true;

    private NPCPatrol patrolScript;

    void Start()
    {
        patrolScript = GetComponent<NPCPatrol>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            // Stop patrol
            if (patrolScript != null)
                patrolScript.enabled = false;

            // Rotate toward player
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), 5f * Time.deltaTime);

            // Chase player if enabled
            if (chasePlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Return to patrol when player leaves area
            if (patrolScript != null)
                patrolScript.enabled = true;
        }
    }
}
