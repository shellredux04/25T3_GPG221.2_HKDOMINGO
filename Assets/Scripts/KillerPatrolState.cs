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
        move?.Patrol();
        // Patrol is the default idle; DON'T call Finish()
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Patrol");
    }
}
