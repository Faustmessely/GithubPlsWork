using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject _spawnPos;

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(this.transform.position.y < - 100)
        { 
            transform.position = _spawnPos.transform.position;
        }
	}



}
