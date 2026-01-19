using System;
using System.Collections.Generic;
using UnityEngine;
using static Nest;

public class Piece : MonoBehaviour
{

    static public Dictionary<string, Piece> pieces;

    public string id;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

    public bool isGrabbed = false;

    private void Awake()
    {
        pieces ??= new Dictionary<string, Piece>();
        //check if id already exists
        if (pieces.ContainsKey(id))
        {
            Debug.LogError($"[GAME] Piece with id {id} already exists!");
            return;
        }
        else
        {
            pieces.Add(id, this);
        }
    }

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
    }

    public void Operate()
    {
        Debug.Log($"[GAME] Piece {id} is being operated.");
        transform.SetPositionAndRotation(startPosition, startRotation);
        transform.localScale = startScale;
    }
}
