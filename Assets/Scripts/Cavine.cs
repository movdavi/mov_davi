using System.Collections.Generic;
using UnityEngine;

public class Cavine : MonoBehaviour
{

    private bool clear = true;
    public bool IsClear()
    {
        return clear;
    }

    // Si es llença un objecte fora la cabina es pot quedar tancat l'operari a dins XD
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("[GAME] Cavine triggered by: " + other.name);
        clear = false;
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("[GAME] Cavine triggered by: " + other.name);

        clear = true;
    }
}
