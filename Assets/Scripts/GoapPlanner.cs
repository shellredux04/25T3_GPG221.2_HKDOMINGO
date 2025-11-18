using UnityEngine;
using System.Collections.Generic;

public class GoapPlanner
{
    public Queue<BaseAction> Plan(
        List<BaseAction> actions,
        WorldState[] world,
        WorldState[] goal)
    {
        List<BaseAction> usableActions = new List<BaseAction>();

        foreach (var a in actions)
        {
            a.DoReset();
            if (a.CheckProceduralPrecondition(null))
                usableActions.Add(a);
        }

        Queue<BaseAction> result = new Queue<BaseAction>();

        foreach (var a in usableActions)
        {
            foreach (var eff in a.effects)
            {
                foreach (var g in goal)
                {
                    if (eff.key == g.key && eff.value == g.value)
                    {
                        result.Enqueue(a);
                    }
                }
            }
        }

        return result;
    }
}
