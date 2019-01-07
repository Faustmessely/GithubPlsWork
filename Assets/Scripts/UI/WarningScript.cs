using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningScript : MonoBehaviour {

    [SerializeField]
    Image _warningSign;

    [SerializeField]
    CheckManager checkManager;
    

    [SerializeField]
    float _flickerSpeed = 0.1f;
    float _timer;
    bool _fadeIn;
    

    private void Update()
    {
        if(checkManager.Warn)
        Flicker();
        else
            _warningSign.CrossFadeAlpha(0, 0, false);
    }

    void Flicker()
    {
        _timer += Time.deltaTime;
        if (_timer < _flickerSpeed)
        {
            if(_fadeIn)
            _warningSign.CrossFadeAlpha(1, _flickerSpeed, false);
            else
                _warningSign.CrossFadeAlpha(0, _flickerSpeed, false);
        }
        else
        {
            _timer = 0;
            if (_fadeIn)
                _fadeIn = false;
            else _fadeIn = true;
        }
    }
}
