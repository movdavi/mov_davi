using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public static Dictionary<NEST_REF, Nest> nests;
    public enum NEST_REF
    {
        NULL,
        NEST_A,
        NEST_B,
        NEST_C,
        NEST_D
    }

    public NEST_REF id;

    private readonly List<Slot.SLOT_REF> slots = new();


    private void Awake()
    {
        nests ??= new Dictionary<NEST_REF, Nest>();
        nests.Add(id, this);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            if (!child.TryGetComponent<Slot>(out Slot slot))
            {
                Debug.Log("Child " + child.name + " is not a Slot.");
            }
            else
            {
                Debug.Log("Adding slot " + slot.id + " to nest " + id);
                slots.Add(slot.id);
            }
        }
    }

    // Update is called once per frame

    public bool IsReady()
    {
        foreach (Slot.SLOT_REF slot_ref in slots)
        {
            if (!Slot.slots[slot_ref].IsReady())
            {
                Debug.Log("Slot " + slot_ref + " is not ready.");
                return false;
            }
        }
        return true;
    }


}
