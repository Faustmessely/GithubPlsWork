using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharkBehaviour : MonoBehaviour {

    [SerializeField]
    UnityEvent[] _phases;
    UnityEvent _currentPhase;
    int _currentIndex = 0;

    [SerializeField]
    UnityEvent _reset;

    [SerializeField]
    SharkActions sharkActions;


    public static bool CanAct = true;

    private void Start()
    {
        _currentPhase = _phases[_currentIndex];
    }

    private void FixedUpdate()
    {
        _currentPhase.Invoke();

        if(sharkActions.GoToNext)
        {
            NextPhase();
        }
    }

    public void NextPhase()
    {
        ResetActions();

        if (_currentIndex < _phases.Length -1)
        { _currentIndex++; }
        else { _currentIndex = 0; }
        
        _currentPhase = _phases[_currentIndex];



    }



    private void ResetActions()
    {
        _reset.Invoke();
    }


}
