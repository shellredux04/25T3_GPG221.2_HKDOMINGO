using UnityEngine;
using Anthill.AI;

public class KillerAttackState : AntAIState
{
    public override void Enter()
    {
        Debug.Log("Killer attacks!");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Finish();
    }
}
