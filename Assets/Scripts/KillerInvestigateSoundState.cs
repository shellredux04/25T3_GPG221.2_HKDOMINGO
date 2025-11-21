using UnityEngine;
using Anthill.AI;

public class KillerInvestigateSoundState : AntAIState
{
    private GameObject owner;
    private Vector3 lastSoundPos;

    public override void Create(GameObject aGameObject)
    {
        owner = aGameObject;
    }

    public void SetSoundPos(Vector3 pos)
    {
        lastSoundPos = pos;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        owner.transform.position = Vector3.MoveTowards(
            owner.transform.position,
            lastSoundPos,
            2f * aDeltaTime
        );

        Finish();
    }
}
