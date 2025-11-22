using UnityEngine;
using Anthill.AI;

public class KillerPatrolState : AntAIState
{
    private KillerMovement move;
    private KillerFeedback feedback;

    public override void Create(GameObject owner)
    {
        move = owner.GetComponent<KillerMovement>();
        feedback = owner.GetComponent<KillerFeedback>();
    }

    public override void Enter()
    {
        feedback?.ShowPatrol();
        Debug.Log("Killer: Enter Patrol");
    }

    public override void Execute(float delta, float timeScale)
    {
        // Walk the patrol points
        move?.Patrol();

        // Allow planner to re-evaluate conditions EVERY tick
        // This is the missing part that prevented state switching.
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Patrol");
    }
}
