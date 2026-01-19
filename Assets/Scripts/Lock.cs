using UnityEngine;
using System.Collections.Generic;

public class Lock : MonoBehaviour
{
    public bool locked = false;

    public void Update()
    {
        if (locked)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public bool IsReady()
    {
        return locked;
    }

    private void OnMouseDown()
    {
        ToggleLock();
    }

    public void OnTriggerEnter(Collider other)
    {
        ToggleLock();
    }


    private void ToggleLock()
    {
        locked = !locked;
    }

    public void Operate()
    {
        locked = false;
    }
}
