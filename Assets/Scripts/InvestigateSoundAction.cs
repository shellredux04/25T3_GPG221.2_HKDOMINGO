using UnityEngine;
using UnityEngine.AI;

public class InvestigateSoundAction : BaseAction
{
    private SoundSensor sound;
    private NavMeshAgent agent;

    private void Start()
    {
        sound = GetComponent<SoundSensor>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool Perform(GameObject agentObj)
    {
        if (sound.lastHeardLocation == null)
        {
            isDone = true;
            return true;
        }

        agent.SetDestination(sound.lastHeardLocation.position);

        if (Vector3.Distance(transform.position, sound.lastHeardLocation.position) < 1.2f)
        {
            isDone = true;
            Destroy(sound.lastHeardLocation.gameObject);
        }

        return true;
    }
}
