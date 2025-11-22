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
        move?.ChasePlayer();
        if (Vector3.Distance(transform.position, move.player.position) < 2.5f)
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Chase");
    }
}
