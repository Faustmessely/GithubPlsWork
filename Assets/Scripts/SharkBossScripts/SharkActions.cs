using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharkActions : MonoBehaviour {

    public CheckManager _checkManager;

    //height management
    [SerializeField]
    float _minHeight;
    [SerializeField]
    float _mediumHeight;
    [SerializeField]
    float _maxHeight;

    Vector3 _targetHeight;

    [SerializeField]
    float _timeToChange = 1f;

    //circling
    [SerializeField]
    Transform _victimLocation;
    [SerializeField]
    float _rotationSpeed;
    Quaternion _targetDirection;

    [SerializeField]
    Transform _circlingPoint;

    //biting
    [SerializeField]
    Transform _left;
    [SerializeField]
    Transform _right;
    [SerializeField]
    Transform _focus;

    [SerializeField]
    UnityEvent _leftPush;
    [SerializeField]
    UnityEvent _rightPush;


    //general
    public bool GoToNext;
    


    bool _resetCircle = true;
    public void Circle()
    {
        if (_resetCircle)
        {

            transform.position = _circlingPoint.position;
            transform.rotation = _circlingPoint.rotation;
            _resetCircle = false;
        }


        SubMergeWhile(SharkObstacleDetection.Obstructed);

       // ManageHeight(_mediumHeight);

        transform.RotateAround(_victimLocation.position, Vector3.up, _rotationSpeed * Time.fixedDeltaTime);
    }


    bool _sidePicked = false;
    float _submergeLiberty = 5f;
    public void Bite()
    {


        if (!_sidePicked)
        {
            PickSide();
            _sidePicked = true;
        }
        if (_sidePicked)
        {

            SubMergeWhile(false);

            if (transform.position.y >= _mediumHeight - _submergeLiberty)
                Push();
        }

    }

    void Push()
    {

            if (_leftSide)
            {
                _leftPush.Invoke();


            }
            else
            {
                _rightPush.Invoke();


            }

            if (SharkObstacleDetection.Obstructed)
                GoToNext = true;
        
    }

    void BitePosition(Transform newPosition)
    {
        transform.position = newPosition.position;
        transform.LookAt(_focus);
    }

    bool _leftSide;
    void PickSide()
    {
        if (_checkManager.AllowLeftTentacle && _checkManager.AllowRightTentacle)
        {
            int side = Random.Range(0, 2);

            if (side == 0)
            {
                BitePosition(_left);
                _leftSide = true;
            }

            else
            {
                BitePosition(_right);
                _leftSide = false;
            }
        }

        else if (_checkManager.AllowLeftTentacle)
        {
            BitePosition(_left);
            _leftSide = true;
        }
        else if (_checkManager.AllowRightTentacle)
        {
            BitePosition(_right);
            _leftSide = false;
        }
        else
        {
            _sidePicked = false;
            GoToNext = true;
            ChangeDirection();
        }
    }


    void ManageHeight(float target)
    {

        _targetHeight = new Vector3(transform.position.x, target, transform.position.z);

   

        transform.position = Vector3.Lerp(transform.position, _targetHeight, _timeToChange * Time.deltaTime);
        
    }

    bool _swapped;
  public void ChangeDirection()
    {
        if (!_swapped)
        {
            _circlingPoint.RotateAround(_circlingPoint.position, _circlingPoint.up, 180f);
            
           _rotationSpeed =- _rotationSpeed;

            Debug.Log(_rotationSpeed);
            _swapped = true;
        }
        
    }
    
    public void SubMergeWhile(bool check)
    {
        if (check)
        {
            ManageHeight(_minHeight);
        }

        if (!check )//&& _submergeActive)
        {
            ManageHeight(_mediumHeight);
        }
    }
    

    public void ResetAll()
    {
        _sidePicked = false;
        _resetCircle = true;
        GoToNext = false;
        _swapped = false;
    }


}
