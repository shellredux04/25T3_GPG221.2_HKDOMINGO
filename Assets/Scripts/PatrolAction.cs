using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : BaseAction
{
    public Transform[] patrolPoints;
    private int index = 0;
    private NavMeshAgent agent;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool Perform(GameObject agentObj)
    {
        agent.SetDestination(patrolPoints[index].position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            index = (index + 1) % patrolPoints.Length;

        return true;
    }
}
