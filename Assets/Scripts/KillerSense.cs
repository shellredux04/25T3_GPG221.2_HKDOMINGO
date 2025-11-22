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

    private Transform t;

    private void Awake()
    {
        t = transform;

        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    public void CollectConditions(AntAIAgent agent, AntAICondition world)
    {
        bool visible = PlayerInSight();
        bool close = PlayerClose();
        bool heard = CanHear();
        bool lowHP = (health <= lowHealthThreshold);

        world.BeginUpdate(agent.planner);
        world.Set("PlayerVisible", visible);
        world.Set("PlayerClose", close);
        world.Set("HeardSound", heard);
        world.Set("LowHealth", lowHP);
        world.EndUpdate();
    }

    private bool PlayerInSight()
    {
        Vector3 toPlayer = player.position - t.position;
        float dist = toPlayer.magnitude;

        if (dist > visionRange) return false;

        float angle = Vector3.Angle(t.forward, toPlayer.normalized);
        if (angle > fov * 0.5f) return false;

        return true;
    }

    private bool PlayerClose()
    {
        return Vector3.Distance(t.position, player.position) <= attackDistance;
    }

    private bool CanHear()
    {
        return Vector3.Distance(t.position, player.position) <= hearingRange;
    }

    private void OnDrawGizmos()
    {
         if (player == null) return;

        Gizmos.color = PlayerInSight() ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, player.position);
    }

}
