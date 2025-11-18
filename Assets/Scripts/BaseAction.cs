using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1f;

    public WorldState[] preconditions;
    public WorldState[] effects;

    protected bool isDone = false;
    protected bool inProgress = false;

    public virtual bool CheckProceduralPrecondition(GameObject agent) { return true; }
    public virtual bool Perform(GameObject agent) { return true; }
    public virtual bool IsDone() { return isDone; }
    public virtual void DoReset() { isDone = false; inProgress = false; }
}

[System.Serializable]
public class WorldState
{
    public string key;
    public bool value;
}
