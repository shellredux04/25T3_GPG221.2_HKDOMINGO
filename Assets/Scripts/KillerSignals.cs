using System;
using UnityEngine;

public static class KillerSignals
{
    public static event Action<Vector3> OnHowl;

    public static void Howl(Vector3 position)
    {
        OnHowl?.Invoke(position);
        Debug.Log($"[KillerSignals] Howl at {position}");
    }
}
