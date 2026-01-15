using UnityEngine;
using System.Collections.Generic;

public class Lock : MonoBehaviour
{
    public enum LOCK_REF
    {
        NULL,
        LOCK_1,
        LOCK_2,
        LOCK_3,
        LOCK_4
    }

    public LOCK_REF id;

    public bool locked = false;

    public static Dictionary<LOCK_REF, Lock> locks;

    public void Awake() {
        locks ??= new Dictionary<LOCK_REF, Lock>();
        locks.Add(id, this);
    }

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


    private void ToggleLock()
    {
        locked = !locked;
    }
}
