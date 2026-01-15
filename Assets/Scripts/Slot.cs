using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum SLOT_REF
    {
        NULL,
        SLOT_A,
        SLOT_B,
        SLOT_C,
        SLOT_D
    }

    public SLOT_REF id;

    public static Dictionary<SLOT_REF, Slot> slots;
    public Piece.PIECE_REF match_piece;
    private Lock.LOCK_REF match_look;

    public Piece.PIECE_REF piece;

    private void Awake()
    {
        slots ??= new Dictionary<SLOT_REF, Slot>();
        slots.Add(SLOT_REF.SLOT_A, this);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        piece = Piece.PIECE_REF.NULL;

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            if (!child.TryGetComponent<Lock>(out Lock lockComponent))
            {
                Debug.Log("Child " + child.name + " is not a Lock.");
            }
            else
            {
                Debug.Log("Assigning lock " + lockComponent.id + " to slot " + id);
                match_look = lockComponent.id;
            }
        }
    }

    public bool IsReady()
    {

        if (match_piece == Piece.PIECE_REF.NULL) {
            Debug.Log("Slot " + this + " has no piece inserted.");
            return false;
        }
        
        if (match_look == Lock.LOCK_REF.NULL) {
            Debug.Log("Slot " + this + " has no match lock assigned.");
            return false;
        }

        if (piece != match_piece)
        {
            Debug.Log("Slot " + this + " has incorrect piece inserted.");
            return false;
        }

        return Lock.locks[match_look].IsReady();
        

    }

    public bool Operate() {
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Piece>(out var pieceComponent))
            return;

        Piece.PIECE_REF piece_ref = pieceComponent.id;

        Debug.Log($"Slot {id} collided with {piece_ref}");

        piece = piece_ref;

    }
}
