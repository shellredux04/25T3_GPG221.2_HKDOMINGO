using UnityEngine;

public class KillerMovement : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float turnSpeed = 6f;

    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    public Transform player;
    public Transform hideSpot;
    public Vector3 soundPos;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    public void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[patrolIndex];
        MoveTo(target.position);

        if (Vector3.Distance(transform.position, target.position) < 0.8f)
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    public void ChasePlayer()
    {
        if (player == null) return;
        MoveTo(player.position);
    }

    public void Investigate()
    {
        MoveTo(soundPos);
    }

    public void Hide()
    {
        if (hideSpot == null) return;
        MoveTo(hideSpot.position);
    }

    private void MoveTo(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;

        // CharacterController-movement
        controller.Move(dir * moveSpeed * Time.deltaTime);

        // rotation
        if (dir != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);
        }
    }
}
