using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSpaceChecker : MonoBehaviour {

    public bool Space = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Space = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
            Space = true;
    }
}
