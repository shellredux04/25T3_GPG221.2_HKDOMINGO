using UnityEngine;
using Anthill.AI;

public class KillerPatrolState : AntAIState
{
    private GameObject owner;

    public override void Create(GameObject aGameObject)
    {
        owner = aGameObject; 
    }

    public override void Enter()
    {
        Debug.Log("Killer: Enter Patrol");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Finish();
    }

    public override void Exit()
    {
        Debug.Log("Killer: Exit Patrol");
    }
}
