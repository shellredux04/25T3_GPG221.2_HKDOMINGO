using UnityEngine;

public enum CivilianState
{
    Patrol,
    Idle,
    RunAway
}

public class CivilianFSM : MonoBehaviour
{
    public CivilianState currentState = CivilianState.Patrol;

    CivilianMovement move;
    CivilianSense sense;

    public float idleTime = 2f;
    float idleTimer = 0f;

    void Awake()
    {
        move = GetComponent<CivilianMovement>();
        sense = GetComponent<CivilianSense>();
    }

    void Update()
    {
        switch (currentState)
        {
            case CivilianState.Patrol:
                move.Patrol();
                if (sense.PlayerVisible) SwitchState(CivilianState.Idle);
                break;

            case CivilianState.Idle:
                move.Idle();
                idleTimer += Time.deltaTime;
                if (sense.PlayerClose)
                    SwitchState(CivilianState.RunAway);
                else if (idleTimer >= idleTime)
                    SwitchState(CivilianState.Patrol);
                break;

            case CivilianState.RunAway:
                move.RunAway();
                if (!sense.PlayerVisible)
                    SwitchState(CivilianState.Patrol);
                break;
        }
    }

    void SwitchState(CivilianState newState)
    {
        currentState = newState;
        idleTimer = 0f;
    }
}
