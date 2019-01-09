using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharkCollisionDetection : MonoBehaviour {

    [SerializeField]
    UnityEvent _onHit;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Bullet")
        {
            _onHit.Invoke();
            Destroy(other.gameObject);
            
        }
    }
}
