using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackTriggerStatus : MonoBehaviour {

    Collider colRackTrigger;
    private Rack parent;
    // Use this for initialization
    void Start () {
        colRackTrigger = GetComponent<Collider>();
        parent = transform.parent.GetComponent<Rack>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnTriggerEnter(Collider col)
    //{
    //    parent.OnChildTriggerEnter(colRackTrigger, col); //pass own collider and the one we've hit
    //}

    //void OnTriggerExit(Collider col)
    //{
    //    parent.OnChildTriggerExit(colRackTrigger, col); // pass the own collider and the one we exit
    //}
}
