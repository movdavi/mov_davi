using UnityEngine;

public class TutorialPieceGrab : MonoBehaviour
{
    private OVRGrabbable grabbable;
    private bool alreadyCounted = false;
    private Blinking blinkable;

    public TutorialManager tutorialManager;

    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        blinkable = GetComponent<Blinking>();
    }

    void Update()
    {
        if (!alreadyCounted && grabbable.isGrabbed)
        {
            alreadyCounted = true;

            tutorialManager.PieceTaken(blinkable);
        }
    }
}
