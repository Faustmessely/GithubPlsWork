using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform VictimLocation;
    [SerializeField]
    float _rotationSpeed;
    Quaternion _targetDirection;


    bool _resetCircle = true;
    public void Circle()
    {
        if (_resetCircle)
        {
            ManageHeight(_mediumHeight);
            _resetCircle = false;
        }

        SubMergeWhile(SharkObstacleDetection.Obstructed);

       // ManageHeight(_mediumHeight);

        transform.RotateAround(VictimLocation.position, Vector3.up, 30 * Time.fixedDeltaTime);
    }

    public void Jump()
    {
       
    }

    public void Bite()
    {

    }


    void ManageHeight(float target)
    {

        _targetHeight = new Vector3(transform.position.x, target, transform.position.z);

   

        transform.position = Vector3.Lerp(transform.position, _targetHeight, _timeToChange * Time.deltaTime);
        
    }

    bool ChangeDirection()
    {
        ManageHeight(_minHeight);
        if (transform.position.y <= _minHeight)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, 0);
            _rotationSpeed = -_rotationSpeed;
            return false;
        }
            return true;
    }


    bool _submergeActive;
    public void SubMergeWhile(bool check)
    {
        if (check)
        {
            ManageHeight(_minHeight);
            _submergeActive = true;
        }

        if (!check && _submergeActive)
        {
            ManageHeight(_mediumHeight);
            if (transform.position.y >= _mediumHeight)
            {
                _submergeActive = false;
            }
        }
    }

    public void ResetAll()
    {
        _resetCircle = true;
    }
       

}
