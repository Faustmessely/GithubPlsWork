using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCannonballLeftScript : MonoBehaviour {
    private float _timer;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-70, 25, 0), ForceMode.VelocityChange);
    }

    void FixedUpdate () {
        _timer++;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -100, 0), ForceMode.Acceleration);
        if (_timer==200)
        {
            Destroy(gameObject);
            _timer = 0;
        }
    }
}
