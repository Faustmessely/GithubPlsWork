using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSpaceChecker : MonoBehaviour {

    public bool Space = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Space = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            Space = true;
    }
}
