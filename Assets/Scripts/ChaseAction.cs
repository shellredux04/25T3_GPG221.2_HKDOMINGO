using UnityEngine;
using UnityEngine.AI;

public class ChaseAction : BaseAction
{
    private NavMeshAgent agent;
    private VisionSensor vision;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<VisionSensor>();
    }

    public override bool Perform(GameObject agentObj)
    {
        if (vision.CanSeeTarget(out Transform t))
        {
            agent.SetDestination(t.position);

            if (Vector3.Distance(transform.position, t.position) < 1.5f)
            {
                isDone = true;
            }
        }
        else
        {
            isDone = true;
        }

        return true;
    }
}
