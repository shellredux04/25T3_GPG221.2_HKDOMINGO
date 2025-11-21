using UnityEngine;
using Anthill.AI;

public class KillerInvestigateSoundState : AntAIState
{
    private KillerMovement move;

    public override void Create(GameObject owner)
    {
        move = owner.GetComponent<KillerMovement>();
    }

    public override void Execute(float delta, float timeScale)
    {
        move.Investigate();
        Finish();
    }
}
