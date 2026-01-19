using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private string id = null;

    public string match_piece = null;

    private string piece = null;

    private List<Lock> locks = new();


    private void Awake()
    {
        if (match_piece == null)
        {
            Debug.LogError("[GAME] Slot " + this + " has no match piece assigned in the inspector.");
        } else
        {
            id = "S_" + match_piece;
        }
    }

    private List<Lock> GetLocks()
    {
        List<Lock> foundLocks = new List<Lock>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Lock>(out Lock lockComponent))
            {
                foundLocks.Add(lockComponent);
            }
        }
        return foundLocks;
    }

    private void Start()
    {
        locks = GetLocks();
    }

    private bool CheckLocks() {
        foreach (Lock slotLock in locks)
        {
            if (!slotLock.IsReady())
            {
                Debug.Log("[GAME] Slot " + id + " has a locked lock: " + slotLock);
                return false;
            }
        }
        return true;
    }
    public bool IsReady()
    {

        if (match_piece == null) {
            Debug.Log("[GAME] Slot " + id + " has no piece inserted.");
            return false;
        }

        if (piece != match_piece)
        {
            Debug.Log("[GAME] Slot " + id + " has incorrect piece inserted.");
            return false;
        }

        return CheckLocks();
    }

    public void Operate() {
        Piece.pieces[match_piece].Operate();
        
        Debug.Log("[GAME] Slot " + id + " operated.");
        
        foreach (Lock slotLock in locks)
        {
            slotLock.Operate();
        }
        piece = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Piece>(out var pieceComponent))
            return;

        string piece_ref = pieceComponent.id;

        Debug.Log($"[GAME] Slot {id} collided with {piece_ref}");

        piece = piece_ref;

    }
}
