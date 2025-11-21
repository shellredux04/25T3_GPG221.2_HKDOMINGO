using UnityEngine;
using Anthill.AI;

public class KillerSense : MonoBehaviour, ISense
{
    [Header("References")]
    public Transform player;              // Drag your Player here, or it will auto-find by tag "Player"

    [Header("Vision Settings")]
    public float visionRange = 12f;       // How far the killer can see
    public float visionAngle = 120f;      // Field of view in degrees

    [Header("Hearing Settings")]
    public float hearingRange = 15f;      // How far the killer can "hear" the player

    [Header("Attack Settings")]
    public float attackRange = 2.5f;      // How close to be considered "PlayerClose"

    [Header("Health Settings")]
    public float health = 100f;
    public float lowHealthThreshold = 30f;

    private Transform _t;

    private void Awake()
    {
        _t = transform;

        // Try to auto-find the player if not assigned
        if (player == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null)
            {
                player = go.transform;
            }
        }
    }

    /// <summary>
    /// Called automatically by AntAIAgent when it wants to update world state.
    /// </summary>
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        if (aAgent == null) return;

        bool isPlayerVisible = CanSeePlayer();
        bool isPlayerClose   = IsPlayerClose();
        bool canHearPlayer   = CanHearPlayer();
        bool isLowHealth     = (health <= lowHealthThreshold);

        // IMPORTANT: wrap Set() calls with BeginUpdate / EndUpdate
        aWorldState.BeginUpdate(aAgent.planner);
        {
            // These strings MUST match your Scenario condition names exactly
            aWorldState.Set("PlayerVisible", isPlayerVisible);
            aWorldState.Set("PlayerClose",   isPlayerClose);
            aWorldState.Set("HeardSound",    canHearPlayer);
            aWorldState.Set("LowHealth",     isLowHealth);
        }
        aWorldState.EndUpdate();
    }

    private bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 toPlayer = player.position - _t.position;
        float distance = toPlayer.magnitude;
        if (distance > visionRange) return false;

        float angle = Vector3.Angle(_t.forward, toPlayer.normalized);
        if (angle > visionAngle * 0.5f) return false;

        // Simple version: no raycast, just distance + angle
        return true;
    }

    private bool IsPlayerClose()
    {
        if (player == null) return false;
        float distance = Vector3.Distance(_t.position, player.position);
        return distance <= attackRange;
    }

    private bool CanHearPlayer()
    {
        if (player == null) return false;
        float distance = Vector3.Distance(_t.position, player.position);
        return distance <= hearingRange;
    }

    // Optional helper if you want to damage the killer from other scripts
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0f) health = 0f;
    }
}
