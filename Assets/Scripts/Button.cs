using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public enum BUTTON_REF
    {
        NULL,
        BUTTON_A,
        BUTTON_B,
        BUTTON_C,
        BUTTON_D
    }

    public BUTTON_REF id;

    public static Dictionary<BUTTON_REF, Button> buttons;

    private Machine.MACHINE_REF machine;

    private void Awake()
    {
        buttons ??= new Dictionary<BUTTON_REF, Button>();
        buttons.Add(id, this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //try get machine from parent
        if (transform.parent != null && transform.parent.TryGetComponent<Machine>(out Machine machineComponent))
        {
            machine = machineComponent.id;
            Debug.Log("Button " + id + " assigned to machine " + machine);
        }
        else
        {
            machine = Machine.MACHINE_REF.NULL;
            Debug.Log("Button " + id + " has no machine assigned.");
        }
    }

    void OnMouseDown()
    {
        OnClick();
    }

    void OnClick()
    {
        if (machine == Machine.MACHINE_REF.NULL)
        {
            Debug.Log($"Button {id} has no machine assigned.");
            return;
        }
        if (!Machine.machines.ContainsKey(machine))
        {
            Debug.Log($"Machine {machine} not found for Button {id}.");
            return;
        }

        Machine.machines[machine].IsReady();
        Debug.Log($"Button clicked: {id}");

    }
}
