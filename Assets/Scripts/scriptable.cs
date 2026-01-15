using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable", menuName = "Scriptable Object/test")]
public class scriptable : ScriptableObject
{

    public enum SCRIPTABLE_REF
    {
        X,
        Y,
        Z
    }

    public SCRIPTABLE_REF id;
    public List<Piece.PIECE_REF> pieces;
    public List<Lock.LOCK_REF> locks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
