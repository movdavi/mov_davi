using System;
using System.Collections.Generic;
using UnityEngine;
using static Nest;

public class Piece : MonoBehaviour
{
    public TutorialManager tutorialManager;
    static public Dictionary<string, Piece> pieces;

    public string id;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

    public bool isGrabbed = false;

    private bool j = true;

    private Blinking blinking;

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
        blinking = TryGetComponent(out Blinking blinkComp) ? blinkComp : null;
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
    }

    private void Update()
    {
        if (isGrabbed && j) {
            j = false;
            PieceGrabbed();
        }
    }

    public void Operate()
    {
        Debug.Log($"[GAME] Piece {id} is being operated.");
        transform.SetPositionAndRotation(startPosition, startRotation);
        transform.localScale = startScale;
    }

    public void PieceGrabbed()
    {
        Debug.Log($"[GAME] Piece {id} has been grabbed.");
        if (tutorialManager != null)
        {
            tutorialManager.PieceTaken(blinking);
        }
        else
        {
            Debug.LogWarning("No TutorialManager found in scene when PieceGrabbed was called.");
        }
    }
}