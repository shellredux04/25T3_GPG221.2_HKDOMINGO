using UnityEngine;
using TMPro;

public class KillerFeedback : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text stateText;     // Drag a TextMeshPro UI object here

    [Header("Creepy Sounds")]
    public AudioSource audioSource;
    public AudioClip patrolSound;
    public AudioClip chaseSound;
    public AudioClip attackSound;
    public AudioClip hideSound;
    public AudioClip investigateSound;

    [Header("Visual Flash")]
    public Renderer bodyRenderer;      // Drag the NPC mesh here
    public Color flashColor = Color.red;
    private Color originalColor;
    private float flashTime = 0.15f;

    private void Start()
    {
        if (bodyRenderer != null)
            originalColor = bodyRenderer.material.color;
    }

    
    private void Display(string message, AudioClip clip)
    {
        if (stateText != null)
            stateText.text = message;

        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);

        if (bodyRenderer != null)
            StartCoroutine(Flash());
    }

    private System.Collections.IEnumerator Flash()
    {
        bodyRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        bodyRenderer.material.color = originalColor;
    }

    // -----------------------
    // PUBLIC FEEDBACK EVENTS
    // -----------------------

    public void ShowPatrol() => Display("PATROLLING", patrolSound);

    public void ShowChase() => Display("CHASING", chaseSound);

    public void ShowAttack() => Display("ATTACKING", attackSound);

    public void ShowHide() => Display("HIDING", hideSound);

    public void ShowInvestigate() => Display("INVESTIGATING NOISE", investigateSound);
}
