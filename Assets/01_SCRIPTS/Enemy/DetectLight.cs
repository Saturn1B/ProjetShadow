using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    public ShadowMovement shadowMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light") || other.CompareTag("Lighter"))
        {
            shadowMovement.detectLight = true;
            shadowMovement.LightSource = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light") || other.CompareTag("Lighter"))
        {
            shadowMovement.detectLight = false;
        }
    }
}
