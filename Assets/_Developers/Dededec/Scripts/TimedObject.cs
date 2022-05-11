using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedObject : MonoBehaviour
{
    /*
    Qué necesito:
    - El intervalo de tiempo entre el que sucede lo que tenga que suceder
    - Una función abstracta en la que se introduzca el comportamiento que tiene
    que suceder entre intervalos.
    */

    [SerializeField] protected float _intervalHours, _intervalMinutes, _intervalSeconds;
    protected float _timeElapsed;

    private void Start() 
    {
        _intervalSeconds += _intervalMinutes * 60f + _intervalHours * 24f * 60f;    
    }

    protected void Update() 
    {
        if(_timeElapsed < _intervalSeconds)
        {
            _timeElapsed += Time.deltaTime;
        }    
        else
        {
            _timeElapsed = 0;
            OnIntervalCompleted();
        }
    }

    protected abstract void OnIntervalCompleted();
}
