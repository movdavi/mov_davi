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
        // Activar highlight del paso actual
        foreach (var piece in steps[index].piecesToHighlight) {
            Debug.Log("Highlighting piece: " + piece.gameObject.name);
            piece.SetHighlight(true);
        }
        var zoneBlink = steps[index].targetZone.GetComponent<Blinking>();
        if (zoneBlink != null)
        {
            Debug.Log("Activando blink en TargetZone");
            zoneBlink.SetHighlight(true);
        }
        if (IsPlaceStep(steps[index]) && steps[index].targetZone != null)
        {
            steps[index].targetZone.SetActive(true);
        }

        Debug.Log("Paso actual: " + steps[index].stepName);
        UpdateProgressText();
    }

    // Llamar desde los triggers / piezas cuando se coloca correctamente
    public void PiecePlaced(Blinking piece, TargetZoneTrigger zone)
    {
        var step = steps[currentStep];
        if (!IsPlaceStep(step))
            return;

        if (zone.gameObject != step.targetZone) {
            Debug.Log("Pieza colocada en zona incorrecta.");
            return;
        }
        bool valid = false;
        foreach (var p in step.piecesToHighlight)
        {
            if (p == piece)
            {
                valid = true;
                break;
            }
        }   
        if (!valid) return;
        
        step.piecesDone++;
        piece.SetHighlight(false);
        if(step.piecesDone >= GetPiecesRequired(step))
        {
            AdvanceStep();
        }
    }

    public void PieceTaken(Blinking piece)
    { 
        var step = steps[currentStep];
        if (!IsTakeStep(step))
            return;

        bool valid = false;
        foreach (var p in step.piecesToHighlight)
        {
            if (p == piece)
            {
                valid = true;
                break;
            }
        }
        if (!valid) return;
        piece.SetHighlight(false);
        step.piecesDone++;
        if (step.piecesDone >= GetPiecesRequired(step))
        {
            AdvanceStep();
        }
    }
    public void AdvanceStep()
    {
        var current = steps[currentStep];
        if (current.targetZone != null)
        {
            var zoneBlink = current.targetZone.GetComponent<Blinking>();
            if (zoneBlink != null)
            {
                zoneBlink.SetHighlight(false);
            }
        }
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