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
        feedback?.ShowHide();
        Debug.Log("Killer: Enter Hide");
    }

    public override void Execute(float delta, float timeScale)
    {
        move?.Hide();
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Hide");
    }
}
