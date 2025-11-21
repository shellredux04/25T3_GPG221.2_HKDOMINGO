using UnityEngine;
using Anthill.AI;

public class KillerChaseState : AntAIState
{
    private KillerMovement move;

    public override void Create(GameObject owner)
    {
        move = owner.GetComponent<KillerMovement>();
    }

    public override void Execute(float delta, float timeScale)
    {
        move.ChasePlayer();
        Finish(); // We want it to move ONE step per update
    }
}
