using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public string id;
    private List<Section> sections;
    private int currentSectionIndex = 0;

    private Cavine cabin_limit = null;
    private Courtain courtain = null;

    private bool isOperating = false;

    public void Start()
    {
        sections = GetSections();
        if (sections.Count > 0)
        {
            sections[0].gameObject.SetActive(true);
        }

        cabin_limit = GetCavine();
        courtain = GetCourtian();
        courtain.gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        
    }

    private Cavine GetCavine()
    {
        if (cabin_limit == null)
        {
            cabin_limit = GetComponentInChildren<Cavine>();
        }
        return cabin_limit;
    }

    private Courtain GetCourtian()
    {
        if (courtain == null)
        {
            courtain = GetComponentInChildren<Courtain>();
        }
        return courtain;
    }

    private List<Section> GetSections()
    {
        List<Section> sections = new();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Section>(out Section section))
            {
                sections.Add(section);
                section.gameObject.SetActive(false);
            }
        }
        return sections;
    }

    public bool Operate()
    {

        if (isOperating) return false;

        if (IsReady())
        {
            StartCoroutine(OperateRoutine());
        }
        else
        {
            Debug.LogWarning($"[GAME] Machine {id} is not ready.");
        }

        return true;
    }

    private IEnumerator OperateRoutine() {
        isOperating = true;

        Debug.Log($"[GAME] Waiting for player to leave cabin...");

        yield return new WaitUntil(() => cabin_limit.IsClear());

        Debug.Log($"[GAME] Showing curtain.");
        courtain.gameObject.SetActive(true);

        Debug.Log($"[GAME] Machine {id} is operating.");

        sections[currentSectionIndex].Operate();
        sections[currentSectionIndex].gameObject.SetActive(false);

        currentSectionIndex = (currentSectionIndex + 1) % sections.Count;

        sections[currentSectionIndex].gameObject.SetActive(true);

        Debug.Log($"[GAME] Operation in progress (Waiting 5s)...");

        yield return new WaitForSeconds(5f);

        Debug.Log($"[GAME] Hiding curtain.");
        courtain.gameObject.SetActive(false);

        isOperating = false;
    }

    public bool IsReady()
    {
        

        if (sections.Count == 0)
        {
            Debug.LogWarning($"[GAME] Machine {id} has no sections.");
            return false;
        }

        if (!sections[currentSectionIndex].IsReady())
        {
            Debug.LogWarning($"[GAME] Machine {id} is not ready because section is not ready.");
            return false;
        }
        
        Debug.Log($"[GAME] Machine {id} is ready.");
        
        return true;
    }
}
