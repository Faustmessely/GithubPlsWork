using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squidhead : MonoBehaviour {
    Healthpoints _healthPoints;
    // Use this for initialization
    void Start () {
        _healthPoints = transform.parent.gameObject.GetComponent<Healthpoints>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet")
        {
                _healthPoints.maxHealth -= other.GetComponent<Bullet>()._bulletDMG;         
        }
        Destroy(other.gameObject);
    }
}
