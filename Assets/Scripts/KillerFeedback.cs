using UnityEngine;
using TMPro;

/// <summary>
/// Handles visible + audio feedback for the killer,
/// and broadcasts creepy "alerts" to other killers instead of radio.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class KillerFeedback : MonoBehaviour
{
    [Header("Visuals")]
    public TMP_Text floatingText;      
    public Renderer bodyRenderer;        

    [Header("Colours per State")]
    public Color patrolColor = Color.green;
    public Color chaseColor = Color.red;
    public Color attackColor = Color.magenta;
    public Color investigateColor = Color.yellow;
    public Color hideColor = Color.cyan;

    [Header("Sounds per State")]
    public AudioClip patrolClip;
    public AudioClip chaseClip;
    public AudioClip attackClip;
    public AudioClip investigateClip;
    public AudioClip hideClip;

    [Header("Alert Communication Sound")]
    public AudioClip alertClip; // creepy scream / howl

    private AudioSource audioSource;
    private KillerSense sense;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sense = GetComponent<KillerSense>();
    }

    // Generic helper
    private void ShowState(string label, Color color, AudioClip clip)
    {
        if (floatingText != null)
            floatingText.text = label;

        if (bodyRenderer != null)
        {
            var mat = bodyRenderer.material;
            mat.color = color;
        }

        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void OnPatrolEnter()
    {
        ShowState("PATROLLING", patrolColor, patrolClip);
    }

    public void OnChaseEnter()
    {
        ShowState("CHASING", chaseColor, chaseClip);
    }

    public void OnAttackEnter()
    {
        ShowState("ATTACK!", attackColor, attackClip);
    }

    public void OnInvestigateEnter()
    {
        ShowState("INVESTIGATE", investigateColor, investigateClip);
    }

    public void OnHideEnter()
    {
        ShowState("HIDING", hideColor, hideClip);
    }

    /// <summary>
    /// Broadcast a creepy scream/howl so other killers react.
    /// </summary>
    public void BroadcastAlert(Vector3 alertPosition)
    {
        if (audioSource != null && alertClip != null)
            audioSource.PlayOneShot(alertClip);

        // Inform all other KillerSense components
        KillerSense[] all = FindObjectsOfType<KillerSense>();
        foreach (var s in all)
        {
            if (s == sense) continue; // don't alert self
            s.ReceiveAlert(alertPosition);
        }
    }
}
