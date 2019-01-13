using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSpaceChecker : MonoBehaviour {

    public bool Space = true;
    float _timer;
    float _timerMax = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Space = false;
            _timer = 0;

        }

    }

    private void FixedUpdate()
    {
        if (_timer >= _timerMax)
        {
            Space = true;

        }
        else
        {
            _timer += Time.fixedDeltaTime;
        }
    }
}
