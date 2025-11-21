using UnityEngine;
using Anthill.AI;

public class KillerHideState : AntAIState
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
            feedback.OnHideEnter();
    }

    public override void Execute(float delta, float timeScale)
    {
        if (move != null)
            move.Hide();

        Finish();
    }
}
