using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigmaInteractable : MonoBehaviour
{

    public string transitionID;

    private void OnTriggerEnter(Collider other)
    {
        // hacky check. in the future, probably best checking for objects in a specific layer
        if (other.gameObject.name == "VRPointerBall" || other.gameObject.name == "card")
        {
            // do transition to next frame if VRPointer hits the collider
            FigmaToCanvas controller = GameObject.FindObjectOfType<FigmaToCanvas>();
            if (transitionID != "-1")
            {
                controller.RenderFrame(transitionID);

                // temp disable trigger to avoid rapid firing of collisions
                other.gameObject.GetComponent<VRPointerController>().DisableSphereForTimePeriod(0.5f);
            }
        }
    }
}
