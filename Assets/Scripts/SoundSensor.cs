using UnityEngine;

public class SoundSensor : MonoBehaviour
{
    public float hearingRadius = 10f;
    public Transform lastHeardLocation;

    public void HearSound(Vector3 pos)
    {
        lastHeardLocation = new GameObject("HeardSound").transform;
        lastHeardLocation.position = pos;
    }
}
