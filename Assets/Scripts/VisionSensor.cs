using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    public float viewDistance = 15f;
    public float viewAngle = 60f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool CanSeeTarget(out Transform target)
    {
        target = null;

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform t = rangeChecks[0].transform;
            Vector3 dir = (t.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < viewAngle / 2)
            {
                float dist = Vector3.Distance(transform.position, t.position);
                if (!Physics.Raycast(transform.position, dir, dist, obstructionMask))
                {
                    target = t;
                    return true;
                }
            }
        }
        return false;
    }
}
