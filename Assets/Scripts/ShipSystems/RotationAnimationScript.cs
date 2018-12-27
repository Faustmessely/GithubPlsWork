using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotationAnimationScript : MonoBehaviour {

    [SerializeField]
    Animator _shipAnimator;

    float _firedTimer;

    [SerializeField]
    float _firedTimerMax = 0.2f;
    

    private void FixedUpdate()
    {
        if (_firedTimer <= 0)
        {
            _shipAnimator.SetBool("Right", false);
            _shipAnimator.SetBool("Left", false);
            _shipAnimator.SetBool("Center", true);

        }
        else _firedTimer-=Time.deltaTime;
    }

    public void GoLeft()
    {
        _shipAnimator.SetBool("Left", true);
        _shipAnimator.SetBool("Right", false);
        _shipAnimator.SetBool("Center", false);

        _firedTimer = _firedTimerMax;
    }

    public void GoRight()
    {
        _shipAnimator.SetBool("Right", true);
        _shipAnimator.SetBool("Left", false);
        _shipAnimator.SetBool("Center", false);
        _firedTimer = _firedTimerMax;
    }

}
