using UnityEngine;

public class AttackAction : BaseAction
{
    public float attackCooldown = 1.5f;
    private float timer = 0f;

    public override bool Perform(GameObject agent)
    {
        timer += Time.deltaTime;

        if (timer >= attackCooldown) 
        {
            Debug.Log("Killer attacks!");
            timer = 0;
            isDone = true;
        }

        return true;
    }
}
