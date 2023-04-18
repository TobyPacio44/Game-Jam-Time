using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUp : MonoBehaviour
{
    public bool isColliding = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            isColliding = false;
        }
    }
}
