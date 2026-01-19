using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    private enum SectionState
    {
        Idle,
        Operating,
        Completed
    }

    private SectionState currentState = SectionState.Idle;
    private List<Nest> GetNests()
    {
        List<Nest> nests = new();

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Nest>(out Nest nest))
            {
                nests.Add(nest);

            }
        }

        return nests;
    }

    public void Operate()
    {
        if (currentState == SectionState.Idle)
        {
            currentState = SectionState.Operating;
        } else if (currentState == SectionState.Idle)
        {
            currentState = SectionState.Idle;
        }
        
        List<Nest> nests = GetNests();
        foreach (Nest nest in nests)
        {
            nest.Opereate();
            Debug.Log("[GAME] Operating nest: " + nest.name);
        }
    }
    public bool IsReady()
    {
        List<Nest> nests = GetNests();

        if (nests.Count == 0) { 
            Debug.LogError("[GAME] Section has no nests!");
            return false;
        }


        foreach (Nest nest in nests)
            if (!nest.IsReady())  return false;

        return true;
    }
}
