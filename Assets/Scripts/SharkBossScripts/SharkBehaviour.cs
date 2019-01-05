using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharkBehaviour : MonoBehaviour {

    [SerializeField]
    UnityEvent[] _phases;
    UnityEvent _currentPhase;
    int _currentIndex = 0;


    public static bool CanAct = true;

    private void Start()
    {
        _currentPhase = _phases[_currentIndex];
    }

    private void Update()
    {
        _currentPhase.Invoke();
    }

    public void NextPhase()
    {
        if (_phases[_currentIndex + 1] != null)
            _currentPhase = _phases[_currentIndex + 1];
        else _currentPhase = _phases[0];

    }




}
