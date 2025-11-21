using UnityEngine;
using Anthill.AI;

public class KillerAttackState : AntAIState
{
    public override void Enter()
    {
        Debug.Log("KILLER ATTACKS PLAYER!");
    }

    public override void Execute(float delta, float timeScale)
    {
        Finish();
    }
}
