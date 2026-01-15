using System.Collections.Generic;
using UnityEngine;
using static Nest;

public class Piece : MonoBehaviour
{

    static public Dictionary<PIECE_REF, Piece> pieces;
    public enum PIECE_REF {
        NULL,
        PIECE_A,
        PIECE_B,
        PIECE_C
    }

    public PIECE_REF id;

    private void Awake()
    {
        pieces ??= new Dictionary<PIECE_REF, Piece>();
        pieces.Add(id, this);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
