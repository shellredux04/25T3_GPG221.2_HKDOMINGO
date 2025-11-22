using UnityEngine;
using Anthill.AI;

public class KillerChaseState : AntAIState
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
        feedback?.ShowChase();
        Debug.Log("Killer: Enter Chase");
    }

    public override void Execute(float delta, float timeScale)
    {
        if (move != null)
            move.ChasePlayer();

        // Let the sensor update world state (PlayerClose) and let the planner
        // pick either Chase again or Attack next tick.
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Chase");
    }
}
