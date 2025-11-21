using UnityEngine;
using Anthill.AI;

public class KillerPatrolState : AntAIState
{
    private KillerMovement move;

    public override void Create(GameObject owner)
    {
        move = owner.GetComponent<KillerMovement>();
    }

    public override void Execute(float delta, float timeScale)
    {
        move.Patrol();
        // Patrol NEVER "finishes" unless you want a timed patrol
        // So we do NOT call Finish() here
    }
}
