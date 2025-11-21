using UnityEngine;
using Anthill.AI;

public class KillerChaseState : AntAIState
{
    private KillerMovement move;
    private KillerFeedback feedback;
    private bool alertSent;

    public override void Create(GameObject owner)
    {
        move = owner.GetComponent<KillerMovement>();
        feedback = owner.GetComponent<KillerFeedback>();
    }

    public override void Enter()
    {
        alertSent = false;
        if (feedback != null)
            feedback.OnChaseEnter();
    }

    public override void Execute(float delta, float timeScale)
    {
        if (move != null)
        {
            move.ChasePlayer();

            // Broadcast a creepy alert once when we start chasing
            if (!alertSent && move.player != null && feedback != null)
            {
                feedback.BroadcastAlert(move.player.position);
                alertSent = true;
            }
        }

        // Single step state – allow planner to re-evaluate conditions
        Finish();
    }
}
