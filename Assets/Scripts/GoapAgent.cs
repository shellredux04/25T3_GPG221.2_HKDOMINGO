using UnityEngine;
using System.Collections.Generic;

public class GoapAgent : MonoBehaviour
{
    public GoapPlanner planner;
    public List<BaseAction> actions = new List<BaseAction>();
    public Queue<BaseAction> actionQueue;

    public WorldState[] worldState;
    public WorldState[] goalState;

    private BaseAction currentAction;

    private void Start()
    {
        planner = new GoapPlanner();
        BaseAction[] acts = GetComponents<BaseAction>();
        foreach (var a in acts)
            actions.Add(a);
    }

    private void Update()
    {
        if (actionQueue == null || actionQueue.Count == 0)
        {
            actionQueue = planner.Plan(actions, worldState, goalState);

            if (actionQueue == null)
                return;
        }

        if (currentAction == null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
        }

        if (currentAction != null && currentAction.Perform(gameObject))
        {
            if (currentAction.IsDone())
            {
                currentAction = null;
            }
        }
    }
}
