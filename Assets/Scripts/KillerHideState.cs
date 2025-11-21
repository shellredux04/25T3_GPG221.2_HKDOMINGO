using UnityEngine;
using Anthill.AI;

public class KillerHideState : AntAIState
{
    private GameObject owner;

    public override void Create(GameObject aGameObject)
    {
        owner = aGameObject;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Finish();
    }
}
