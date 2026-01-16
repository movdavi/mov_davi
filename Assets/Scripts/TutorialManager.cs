using System;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialStep[] steps;
    private int currentStep = 0;

    [Header("UI")]
    public TMP_Text progressText;

    public event Action<float> ProgressChanged;
    public float ProgressPercent { get; private set; }

    void Start()
    {
        StartStep(currentStep);
    }

    //Se llama cuando le damos al boton empezar del maim
    void StartStep(int index)
    {
        // Apagar todos los highlights primero
        foreach (var step in steps)
        {
            step.piecesDone = 0;
            foreach (var piece in step.piecesToHighlight)
                piece.SetHighlight(false);
            if (step.targetZone != null)
                step.targetZone.SetActive(false);
        }

        // Activar highlight del paso actual
        foreach (var piece in steps[index].piecesToHighlight)
            piece.SetHighlight(true);

        if (IsPlaceStep(steps[index]) && steps[index].targetZone != null)
        {
            steps[index].targetZone.SetActive(true);
            if (steps[index].targetZone.TryGetComponent<TargetZoneTrigger>(out var trigger))
                trigger.tutorialManager = this;
        }

        Debug.Log("Paso actual: " + steps[index].stepName);
        UpdateProgressText();
    }

    // Llamar desde los triggers / piezas cuando se coloca correctamente
    public void PiecePlaced(GameObject piece)
    {
        var step = steps[currentStep];
        if (!IsPlaceStep(step))
            return;

        bool valid = false;
        foreach (var p in step.piecesToHighlight)
        {
            if (p.gameObject == piece)
            {
                valid = true;
                break;
            }
        }   
        if (!valid) return;
        
        step.piecesDone++;
        if(step.piecesDone >= GetPiecesRequired(step))
        {
            AdvanceStep();
        }
    }

    public void PieceTaken(GameObject piece)
    {
        var step = steps[currentStep];
        if (!IsTakeStep(step))
            return;

        bool valid = false;
        foreach (var p in step.piecesToHighlight)
        {
            if (p.gameObject == piece)
            {
                valid = true;
                break;
            }
        }
        if (!valid) return;

        step.piecesDone++;
        if (step.piecesDone >= GetPiecesRequired(step))
        {
            AdvanceStep();
        }
    }
    public void AdvanceStep()
    {
        currentStep++;
        if (currentStep < steps.Length)
            StartStep(currentStep);
        else
            Debug.Log("Tutorial completado!");

        UpdateProgressText();
    }

    private bool IsTakeStep(TutorialStep step)
    {
        return !string.IsNullOrEmpty(step.type) && step.type.ToLowerInvariant() == "take";
    }

    private bool IsPlaceStep(TutorialStep step)
    {
        return string.IsNullOrEmpty(step.type) || step.type.ToLowerInvariant() == "place";
    }

    private int GetPiecesRequired(TutorialStep step)
    {
        return step.piecesRequired > 0 ? step.piecesRequired : step.piecesToHighlight.Length;
    }

    private void UpdateProgressText()
    {
        var totalSteps = steps != null ? steps.Length : 0;
        float percent = totalSteps > 0 ? (float)currentStep / totalSteps : 0f;
        percent = Mathf.Clamp01(percent) * 100f;
        ProgressPercent = percent;
        ProgressChanged?.Invoke(ProgressPercent);

        if (progressText != null)
            progressText.text = $"Progrés: {ProgressPercent:0}%";
    }
}