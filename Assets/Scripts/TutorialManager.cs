using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialStep[] steps;
    private int currentStep = 0;

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
            foreach (var piece in step.piecesToHighlight)
                piece.SetHighlight(false);
        }

        // Activar highlight del paso actual
        foreach (var piece in steps[index].piecesToHighlight)
            piece.SetHighlight(true);

        Debug.Log("Paso actual: " + steps[index].stepName);
    }

    // Llamar desde los triggers / piezas cuando se coloca correctamente
    public void PiecePlaced(GameObject piece)
    {
        var step = steps[currentStep];
        bool valid = false;
        foreach (var p in step.piecesToHighlight)
        {
            if (p.gameObject == piece)
            valid = true;
            break;
        }   
        if (!valid) return;
        
        step.piecesDone++;
        if(step.piecesDone >= step.piecesToHighlight.Length)
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
    }
}
