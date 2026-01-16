using TMPro;
using UnityEngine;

public class TutorialProgressText : MonoBehaviour
{
    [Header("References")]
    public TutorialManager tutorialManager;
    public TMP_Text progressText;

    private void Awake()
    {
        if (progressText == null)
            progressText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        if (tutorialManager == null)
            return;

        tutorialManager.ProgressChanged += HandleProgressChanged;
        HandleProgressChanged(tutorialManager.ProgressPercent);
    }

    private void OnDisable()
    {
        if (tutorialManager == null)
            return;

        tutorialManager.ProgressChanged -= HandleProgressChanged;
    }

    private void HandleProgressChanged(float percent)
    {
        if (progressText == null)
            return;

        progressText.text = $"Progrés: {percent:0}%";
    }
}
