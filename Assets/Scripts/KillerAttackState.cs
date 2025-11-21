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
        Debug.Log("KILLER ATTACKS PLAYER!");
        if (feedback != null)
            feedback.OnAttackEnter();
    }

    public override void Execute(float delta, float timeScale)
    {
        // You could apply damage to player here
        Finish();
    }
}
