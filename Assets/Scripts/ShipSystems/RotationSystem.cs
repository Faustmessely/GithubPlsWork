using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : MonoBehaviour {

    [SerializeField]
    CheckManager _checkManager;

    [SerializeField]
    float _rotation;

    float _maxRotation = 50;
    float _minRotation = -50;
    float _initialTurnSpeed = 0.1f;
    float _briskTurnSpeed = 5;
    float _turnSpeed = 0.1f;
    
    public static float Rotation;

    float _returnTimer;
    float _returnTimerMax = 3;

    float _turnInterval = 5;
    float _returnInterval = 2;
    float _briskRotation = 20;

    bool _correctionInProgress;
    float BounceSpeed = 10;

    private void Start()
    {
        _turnSpeed = _initialTurnSpeed;
    }


    void FixedUpdate()
    {
        RecalculateRotation();
        ReturnRotation();
        CorrectCourse();
    }

    public void TurnLeft()
    {
        if (!_correctionInProgress)
        {
            _rotation += _turnInterval;
            _returnTimer = _returnTimerMax;
        }
    }

    public void TurnRight()
    {
        if (!_correctionInProgress)
        {
            _rotation -= _turnInterval;
        _returnTimer = _returnTimerMax;
        }
    }

    void RecalculateRotation()
    {
        //prevent exceeding boundaries
        _rotation = Mathf.Clamp(_rotation, _minRotation, _maxRotation);
       
        
        //change static variable and smoothen
        Rotation = Mathf.Lerp(Rotation, _rotation, _turnSpeed * Time.deltaTime);
        
        
        //Change rotation
        transform.rotation = Quaternion.Euler(0, Rotation, 0);
    }

    void ReturnRotation()
    {
        if (Rotation != _rotation && _returnTimer <= 0)
        {
            if (_rotation > 0)
            {
                _rotation -= _returnInterval;
            }
            else if (_rotation < 0)
            {
                _rotation += _returnInterval;
            }

            if (Mathf.Abs(0 - _rotation) < _returnInterval)
                _rotation = 0;
        }
        else
            _returnTimer -= Time.deltaTime;
    }
    
    void CorrectCourse()
    {
        if(_checkManager != null)
        {
            if (_checkManager.ShipWallCollision && !_correctionInProgress)
            {
                TileMotionBehaviour.MovementSpeed = BounceSpeed;
                _turnSpeed = _briskTurnSpeed;

                if (Rotation > 0)
                    _rotation = -_briskRotation;
                if (Rotation < 0)
                    _rotation = _briskRotation;

                _correctionInProgress = true;
            }

            if(!_checkManager.ShipWallCollision)
            {
                TileMotionBehaviour.MovementSpeedReset();
                _correctionInProgress = false;
                _turnSpeed = _initialTurnSpeed;
            }
        }
        
    }
}
