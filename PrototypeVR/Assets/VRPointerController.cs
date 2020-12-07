using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPointerController : MonoBehaviour
{
    public Transform controller;

    public void DisableSphereForTimePeriod(float time)
    {
        SetTriggerActive(false);
        Invoke("SetTriggerActive", time);
    }

    private void SetTriggerActive(bool active)
    {
        this.GetComponent<Collider>().isTrigger = active;
    }

    private void SetTriggerActive()
    {
        SetTriggerActive(true);
    }
}
