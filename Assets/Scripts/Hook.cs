using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    bool _activated = false;
    float _timer = 0;
    public int spawnTime = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_activated)//activeer bool op input
        {
            _timer += Time.deltaTime;
        }

        if(_timer >= spawnTime)
        {
            //speel animatie
            //add item 2 array
            _timer = 0;//resettimer
            _activated = false;
        }

	}
}
