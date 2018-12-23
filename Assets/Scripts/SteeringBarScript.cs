using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SteeringBarScript : MonoBehaviour {

    private float _maxRotation;
    private float _minRotation;
    private float _currentRotation;
    private float _scrollBarValue;

    public GameObject Turning;

    // Use this for initialization
    void Start () {
        _minRotation = Turning.GetComponent<RotationSystem>()._minRotation;
        _maxRotation = Turning.GetComponent<RotationSystem>()._maxRotation;
    }
	
	// Update is called once per frame
	void Update () {
        _currentRotation = Turning.GetComponent<RotationSystem>()._rotation;
        this.gameObject.GetComponent<Scrollbar>().value = (_currentRotation+50)/100;
    }
}
