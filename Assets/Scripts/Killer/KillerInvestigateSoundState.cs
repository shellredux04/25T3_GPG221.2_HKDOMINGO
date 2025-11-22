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
        feedback?.ShowInvestigate();
        Debug.Log("Killer: Enter Investigate");
    }

    public override void Execute(float delta, float timeScale)
    {
        if (move != null)
            move.Investigate();

        // One step of investigate, then allow planner to re-evaluate.
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Investigate");
    }
}
