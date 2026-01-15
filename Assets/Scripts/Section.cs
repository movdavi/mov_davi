using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public enum SECTION_REF
    {
        NULL,
        SECTION_A,
        SECTION_B,
        SECTION_C,
        SECTION_D
    }

    public SECTION_REF id;

    public static Dictionary<SECTION_REF, Section> sections;

    private readonly List<Nest.NEST_REF> nests = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            if (!child.TryGetComponent<Nest>(out Nest nest))
            {
                Debug.Log("Child " + child.name + " is not a Nest.");
            }
            else
            {
                Debug.Log("Adding nest " + nest.id + " to section " + id);
                nests.Add(nest.id);
            }
        }
    }

    private void Awake()
    {
        sections ??= new Dictionary<SECTION_REF, Section>();
        sections.Add(id, this);
    }
    public bool IsReady()
    {

        foreach (Nest.NEST_REF nest_ref in nests)
        {
            if (!Nest.nests[nest_ref].IsReady())
            {
                Debug.Log("Nest " + nest_ref + " is not ready.");
                return false;
            }
        }
        return true;
    }
}
