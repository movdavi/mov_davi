using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private readonly float press_depth = 0.08f;

    private Machine machine = null;

    private GameObject button_box = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.childCount > 0)
            button_box = transform.GetChild(0).gameObject;
        else
            Debug.LogError("[GAME] This button has no visual child (Button Box) to move!");

        if (!transform.parent.TryGetComponent<Machine>(out machine))
            Debug.LogError("[GAME] Button component not found on GameObject.");
    }

    void OnMouseDown()
    {
        OnClick();
    }

    void OnClick()
    {
        if (machine != null)
        {
            Debug.Log("[GAME] Button clicked, checking machine readiness...");
            machine.Operate();
        }
        else
            Debug.LogError("[GAME] Machine component not found on parent GameObject.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[GAME] Button pressed via trigger.");
        if (button_box != null)
            button_box.transform.localPosition -= new Vector3(0, press_depth, 0);
        OnClick();
    }

    private void OnTriggerExit(Collider other)
    {
        if (button_box != null)
            button_box.transform.localPosition += new Vector3(0, press_depth, 0);
    }
}
