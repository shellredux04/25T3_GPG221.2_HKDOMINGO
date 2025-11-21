using UnityEngine;
using Anthill.AI;

public class KillerSense : MonoBehaviour, ISense
{
    [Header("References")]
    public Transform player;

    [Header("Vision Settings")]
    public float visionRange = 12f;
    public float visionAngle = 120f;

    [Header("Hearing Settings")]
    public float hearingRange = 15f;

    [Header("Attack Settings")]
    public float attackRange = 2.5f;

    [Header("Health Settings")]
    public float health = 100f;
    public float lowHealthThreshold = 30f;

    [Header("Alert Settings")]
    public float alertMemoryTime = 5f; // how long an alert is remembered

    private Transform _t;
    private KillerMovement move;

    // external alert (from another killer)
    private bool heardExternalAlert;
    private Vector3 lastAlertPos;
    private float alertTimer;

    private void Awake()
    {
        _t = transform;
        move = GetComponent<KillerMovement>();

        if (player == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null) player = go.transform;
        }
    }

    private void Update()
    {
        // decay alert over time
        if (heardExternalAlert)
        {
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0f)
            {
                heardExternalAlert = false;
            }
        }
    }

    // Called by KillerFeedback.BroadcastAlert
    public void ReceiveAlert(Vector3 pos)
    {
        heardExternalAlert = true;
        lastAlertPos = pos;
        alertTimer = alertMemoryTime;

        if (move != null)
            move.soundPos = pos;
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        if (aAgent == null) return;

        bool isPlayerVisible = CanSeePlayer();
        bool isPlayerClose   = IsPlayerClose();
        bool canHearPlayer   = CanHearPlayer();
        bool isLowHealth     = (health <= lowHealthThreshold);

        // HeardSound is true if we can hear OR heard an alert
        bool heardSound = canHearPlayer || heardExternalAlert;

        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set("PlayerVisible", isPlayerVisible);
            aWorldState.Set("PlayerClose",   isPlayerClose);
            aWorldState.Set("HeardSound",    heardSound);
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

        // You can add a Physics.Raycast here if you want obstacles
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

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0f) health = 0f;
    }

    // Debug gizmos
    private void OnDrawGizmosSelected()
    {
        if (_t == null) _t = transform;

        // Vision range
        Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
        Gizmos.DrawWireSphere(_t.position, visionRange);

        // Hearing range
        Gizmos.color = new Color(1f, 1f, 0f, 0.25f);
        Gizmos.DrawWireSphere(_t.position, hearingRange);

        // Forward direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_t.position, _t.position + _t.forward * visionRange);
    }
}
