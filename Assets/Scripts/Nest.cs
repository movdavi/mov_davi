using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    Piece result_piece = null;

    private void Start()
    {
        result_piece = GetResultPiece();
        if (result_piece == null)
            Debug.LogError("[GAME] Nest has no result piece!");
        else
            result_piece.gameObject.SetActive(false);
    }

    private Piece GetResultPiece()
    {
        Piece res = null;
        foreach (Transform child in transform)
            if (child.TryGetComponent<Piece>(out Piece piece))
                res = piece;
        return res;
    }
    private List<Slot> GetSlots()
    {
        List<Slot> slots = new();

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Slot>(out Slot slot))
            {
                slots.Add(slot);
            }
        }

        return slots;
    }
    public bool IsReady()
    {
        List<Slot> slots = GetSlots();

        if (slots.Count == 0) 
            Debug.LogError("[GAME] Nest has no slots!");
        
        foreach (Slot slot in slots)
            if (!slot.IsReady()) return false;
            
        return true;
    }

    public void Opereate()
    {
        List<Slot> slots = GetSlots();
        foreach (Slot slot in slots)
        {
            slot.Operate();
            Debug.Log("[GAME] Slot operated: " + slot.name);
        }

        if (result_piece != null) { 
            result_piece.gameObject.SetActive(true);
            result_piece.Operate();
        }
    }
}
