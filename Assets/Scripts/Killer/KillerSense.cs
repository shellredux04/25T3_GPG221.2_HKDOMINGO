using UnityEngine;
using Anthill.AI;

public class KillerSense : MonoBehaviour, ISense
{
    public Transform player;

    [Header("Vision")]
    public float visionRange = 16f;
    public float fov = 160f;

    [Header("Hearing")]
    public float hearingRange = 20f;

    [Header("Attack")]
    public float attackDistance = 3f;

    [Header("Health")]
    public float health = 100;
    public float lowHealthThreshold = 40;

    private void Awake()
    {
        if (player == null)
        {
            GameObject go = GameObject.FindWithTag("Player");
            if (go != null)
            {
                player = go.transform;
            }
        }
    }

    public void CollectConditions(AntAIAgent agent, AntAICondition world)
    {
        bool visible = PlayerInSight();
        bool close   = PlayerClose();
        bool heard   = CanHear();
        bool lowHP   = (health <= lowHealthThreshold);

        world.BeginUpdate(agent.planner);
        world.Set("PlayerVisible", visible);
        world.Set("PlayerClose",   close);
        world.Set("HeardSound",    heard);
        world.Set("LowHealth",     lowHP);
        world.EndUpdate();
    }

    private bool PlayerInSight()
    {
        if (player == null) return false;

        Vector3 toPlayer = player.position - transform.position;
        float dist = toPlayer.magnitude;

        if (dist > visionRange) return false;

        float angle = Vector3.Angle(transform.forward, toPlayer.normalized);
        if (angle > fov * 0.5f) return false;

        return true;
    }

    private bool PlayerClose()
    {
        if (player == null) return false;

        return Vector3.Distance(transform.position, player.position) <= attackDistance;
    }

    private bool CanHear()
    {
        if (player == null) return false;

        return Vector3.Distance(transform.position, player.position) <= hearingRange;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = PlayerInSight() ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
