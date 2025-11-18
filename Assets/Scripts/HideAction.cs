using UnityEngine;
using UnityEngine.AI;

public class HideAction : BaseAction
{
    public Transform hideSpot;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool Perform(GameObject agentObj)
    {
        agent.SetDestination(hideSpot.position);

        if (Vector3.Distance(transform.position, hideSpot.position) < 1f)
        {
            isDone = true;
        }

        return true;
    }
}
