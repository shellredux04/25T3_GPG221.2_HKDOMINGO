using UnityEngine;
using Anthill.AI;

public class KillerInvestigateSoundState : AntAIState
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
        if (feedback != null)
            feedback.OnInvestigateEnter();
    }

    public override void Execute(float delta, float timeScale)
    {
        if (move != null)
            move.Investigate();

        Finish();
    }
}
