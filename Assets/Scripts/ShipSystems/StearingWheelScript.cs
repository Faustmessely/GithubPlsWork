using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StearingWheelScript : MonoBehaviour {
    
    Quaternion _rotationIncremented;

    [SerializeField]
    float _ultimate = 49;

    [SerializeField]
    float _incrementForRotation = 30;
    [SerializeField]
    float _speed = 100;

    bool _allowRotate;



    private void Update()
    {
        if (RotationSystem.Rotation >= _ultimate || RotationSystem.Rotation <= -_ultimate)
        {
            _allowRotate = false;
        }
        else _allowRotate = true;
    }

    public void RotateLeft()
    {
        if (_allowRotate)
        {
            _rotationIncremented = Quaternion.AngleAxis(_incrementForRotation, Vector3.forward) * this.transform.rotation;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _rotationIncremented, Time.deltaTime * _speed);
        }
    }

    public void RotateRight()
    {
        if (_allowRotate)
        {
            _rotationIncremented = Quaternion.AngleAxis(-_incrementForRotation, Vector3.forward) * this.transform.rotation;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _rotationIncremented, Time.deltaTime * _speed);
        }
    }
}
