using UnityEngine;

public class CivilianSense : MonoBehaviour
{
    public Transform player;
    public float visionRange = 10f;
    public float runDistance = 6f;

    public bool PlayerVisible { get; private set; }
    public bool PlayerClose   { get; private set; }

    void Awake()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        PlayerVisible = dist <= visionRange;
        PlayerClose   = dist <= runDistance;
    }
}
