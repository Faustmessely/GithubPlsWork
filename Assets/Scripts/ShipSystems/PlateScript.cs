using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlateScript : MonoBehaviour {

    [SerializeField]
    UnityEvent _executedEvent;


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _executedEvent.Invoke();
        }
    }

}
