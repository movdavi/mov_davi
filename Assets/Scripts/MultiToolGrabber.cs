using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiToolGrabber : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Which controller to listen to (L Touch or R Touch)")]
    public OVRInput.Controller right = OVRInput.Controller.RTouch;

    private readonly float detectionRadius = 0.05f;
    public LayerMask searchLayer;

    [Tooltip("Which button to listen for")]
    public OVRInput.Button handTrigger = OVRInput.Button.PrimaryHandTrigger;
    public OVRInput.Button indexTrigger = OVRInput.Button.PrimaryIndexTrigger;

    private readonly Dictionary<OVRInput.Button, Piece> pieces_grabbed = new();
    private readonly Dictionary<OVRInput.Button, Transform> last_parent = new();


    private void Attach_Piece(OVRInput.Button button, Piece piece)
    {
        Debug.Log($"[GAME] grabbed piece: {piece.id}");
        pieces_grabbed.Add(button, piece);
        last_parent.Add(button, piece.gameObject.transform.parent);
        piece.gameObject.transform.SetParent(transform, true);
        piece.isGrabbed = true;
        piece.PieceGrabbed();
    }
    private void Detach_Piece(OVRInput.Button button)
    {

        if (pieces_grabbed.TryGetValue(button, out Piece piece))
        {
            Debug.Log("[GAME] dropped piece");
            piece.transform.SetParent(last_parent[button]);
            piece.isGrabbed = false;
            pieces_grabbed.Remove(button);
            last_parent.Remove(button);
        }
        else return;
    }

    void Update()
    {
        // 1. Detect Initial Press
        if (OVRInput.GetDown(indexTrigger, right))
        {
            Debug.Log($"[GAME] Button Pressed: {indexTrigger}");
           
            Piece piece = FindNearest();
            
            Debug.Log($"[GAME] Nearest Piece: {(piece != null ? piece.id : "None")}");
            Attach_Piece(indexTrigger, piece);
        }

        if (OVRInput.GetDown(handTrigger, right))
        {
            Debug.Log($"[GAME] Button Pressed: {handTrigger}");

            Piece piece = FindNearest();

            Debug.Log($"[GAME] Nearest Piece: {(piece != null ? piece.id : "None")}");

            Attach_Piece(handTrigger, piece);

        }

        // 2. Detect Release
        if (OVRInput.GetUp(indexTrigger, right))
        {
            Debug.Log($"[GAME] Button Released: {indexTrigger}");
            Detach_Piece(indexTrigger);
        }

        if (OVRInput.GetUp(handTrigger, right))
        {
            Debug.Log($"[GAME] Button Released: {handTrigger}");
            Detach_Piece(handTrigger);
        }
    }

    Piece FindNearest()
    {
        const int maxColliders = 32;
        Collider[] hits = new Collider[maxColliders];
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hits, searchLayer.value);

        Piece closest = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = hits[i];
            if (hit.gameObject == gameObject) continue;
            if (!hit.TryGetComponent<Piece>(out Piece self_piece)) continue;
            else
            {
                if (self_piece.isGrabbed) continue;

                float dist = (hit.transform.position - transform.position).sqrMagnitude;
                if (dist < minDistance)
                {
                    closest = self_piece;
                    minDistance = dist;
                }
            }

            return closest;
        }

        return null;


    }

    // VISUALIZATION: Draw the search circle in the Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
