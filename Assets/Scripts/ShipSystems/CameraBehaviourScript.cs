using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{

    [SerializeField]
    Transform _target;

    [SerializeField]
    float _defaultDistance;

    [SerializeField]
    float _rotX;

    void Start()
    {

        if (_target == null)
        {
            Debug.Log("Target not acquired");
        }
        transform.rotation = Quaternion.Euler(_rotX, 0, 0);

        transform.position = _target.position + CalculateDistance(_rotX, _defaultDistance);

    }

    Vector3 CalculateDistance(float angle, float distance)
    {
        float newAngle = 90 - angle;
        float newZ = distance * Mathf.Sin(newAngle / 360 * 2 * Mathf.PI);
        float newY = distance * Mathf.Cos(newAngle / 360 * 2 * Mathf.PI);
        return new Vector3(0, newY, -newZ);
    }

    void Update()
    {
        Debug.DrawLine(transform.position, _target.position, Color.red);


    }
}
