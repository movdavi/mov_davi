using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TargetZoneTrigger : MonoBehaviour
{
    [Tooltip("Reference to the tutorial manager in the scene.")]
    public TutorialManager tutorialManager;

    private Blinking Blinking;

    private void Reset()
    {
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TargetZoneTrigger: OnTriggerEnter with " + other.gameObject.name);
        if (tutorialManager == null)
        {
            Debug.LogWarning($"TargetZoneTrigger on {name} has no TutorialManager assigned.");
            return;
        }
        Blinking = other.GetComponent<Blinking>();
        Blinking.SetHighlight(false);
        if (!other.TryGetComponent<Blinking>(out var pieceComponent))
            return;

        tutorialManager.PiecePlaced(pieceComponent, this);
    }
}