using UnityEngine;

[System.Serializable]
public class TutorialStep
{
    public string type; //Tipus per saber si s'ha d'agafar una peça, posar-la o accionar alguna cosa (ex: "take", "place", "activate")
    public string stepName;
    public Blinking[] piecesToHighlight; //per si de cas hi ha varies peces a destacar
    public GameObject targetZone;
    public int piecesRequired = 1; 
    [HideInInspector]
    public int piecesDone = 0;
}
