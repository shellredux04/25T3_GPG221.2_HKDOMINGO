using UnityEngine;
using Anthill.AI;

public class KillerAttackState : AntAIState
{
    private KillerFeedback feedback;

    public override void Create(GameObject owner)
    {
        feedback = owner.GetComponent<KillerFeedback>();
    }

    public override void Enter()
    {
        feedback?.ShowAttack();
        Debug.Log("KILLER ATTACKS PLAYER!");
    }

    public override void Execute(float delta, float timeScale)
    {
        // You can apply damage to the player here.
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Attack");
    }
}
