using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlateScript : MonoBehaviour {

    [SerializeField]
    UnityEvent _executedEvent;

    bool _execute = false;

    int _playerCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _playerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCount--;
        }
    }


    private void FixedUpdate()
    {
       for(int i = 0; i < _playerCount; i++)
        _executedEvent.Invoke();

        //Debug.Log("count " + _playerCount);
    }

  

}
