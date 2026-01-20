using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TargetZoneTrigger : MonoBehaviour
{
    [Tooltip("Reference to the tutorial manager in the scene.")]
    public TutorialManager tutorialManager;

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
        if (tutorialManager == null)
        {
            Debug.LogWarning($"TargetZoneTrigger on {name} has no TutorialManager assigned.");
            return;
        }

        if (!other.TryGetComponent<Blinking>(out var pieceComponent))
            return;

        tutorialManager.PiecePlaced(pieceComponent);
    }
}