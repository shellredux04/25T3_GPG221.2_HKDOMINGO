using UnityEngine;
using Anthill.AI;

public class KillerChaseState : AntAIState
{
    private GameObject owner;
    private Transform target;

    public override void Create(GameObject aGameObject)
    {
        owner = aGameObject;
    }

    public override void Enter()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if (target == null)
        {
            Finish();
            return;
        }

        owner.transform.position = Vector3.MoveTowards(
            owner.transform.position,
            target.position,
            3f * aDeltaTime
        );

        Finish();
    }
}
