using System;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{

    public enum MACHINE_REF
    {
        NULL,
        MACHINE_A,
        MACHINE_B,
        MACHINE_C,
        MACHINE_D
    }

    public MACHINE_REF id;
        

    public static Dictionary<MACHINE_REF, Machine> machines;
    private readonly List<Section.SECTION_REF> sections = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            if (!child.TryGetComponent<Section>(out Section section))
            {
                Debug.Log("Child " + child.name + " is not a Section.");
            }
            else
            {
                Debug.Log("Adding section " + section.id + " to machine " + id);
                sections.Add(section.id);
            }
        }
    }


    private void Awake()
    {
        machines ??= new Dictionary<MACHINE_REF, Machine>();
        machines.Add(id, this);
    }

    public bool IsReady()
    {

        foreach (Section.SECTION_REF section_ref in sections)
        {
            if (!Section.sections[section_ref].IsReady())
            {
                Debug.Log("Section " + section_ref + " is not ready.");
                return false;
            }
        }
        Debug.Log("Machine " + id + " is ready.");
        return true;
    }
}
