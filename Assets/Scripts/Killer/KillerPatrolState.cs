using UnityEngine;
using Anthill.AI;

public class KillerPatrolState : AntAIState
{
    private KillerMovement move;
    private KillerFeedback feedback;
    private KillerSense sense;

    public override void Create(GameObject owner)
    {
        move     = owner.GetComponent<KillerMovement>();
        feedback = owner.GetComponent<KillerFeedback>();
        sense    = owner.GetComponent<KillerSense>();
    }

    public override void Enter()
    {
        feedback?.ShowPatrol();
        Debug.Log("Killer: Enter Patrol");
    }

    public override void Execute(float delta, float timeScale)
    {
        // keep walking between patrol points
        move?.Patrol();

        // IMPORTANT: if we can see the player, stop this action
        if (sense != null && sense.PlayerVisible)
        {
            Debug.Log("Killer: Player seen during Patrol → request new plan");
            Finish();   // tells AntAI to end this action so planner can switch to Chase
        }
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Patrol");
    }
}
