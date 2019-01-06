using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkObstacleDetection : MonoBehaviour {

    public static bool Obstructed;
    float _timer;

    [SerializeField]
    float _timerMax = 2f;


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Obstacle") || other.CompareTag("Wall"))
        {
            Obstructed = true;
            _timer = _timerMax;
        }
    }

    private void FixedUpdate()
    {
        if (_timer <= 0)
        {
            Obstructed = false;
        }
        else _timer -= Time.fixedDeltaTime;
    }
}
