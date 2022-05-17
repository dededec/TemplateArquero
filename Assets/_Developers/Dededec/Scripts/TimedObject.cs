using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedObject : MonoBehaviour
{
    [Header("Interval Settings")]
    [SerializeField] protected float _intervalDays;
    [SerializeField] protected float _intervalHours;
    [SerializeField] protected float _intervalMinutes; 
    [SerializeField] protected float _intervalSeconds;
    [SerializeField] protected TimeManager _timeManager;
    protected float _timeElapsed;
    protected float _totalSeconds;

    private void Start() 
    {
        _totalSeconds = _intervalSeconds + _intervalMinutes * 60f + _intervalHours * 3600f + _intervalDays * 86400f;
        Initialize();
        /*
        ? Debería meter aquí que si ha pasado tiempo desde
        ? la ultima conexión se hagan OnIntervalCompleted las veces que haga falta?
        */    
    }

    protected void Update() 
    {
        if(_timeElapsed < _totalSeconds)
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

    
    // <summary>
    // Función ejecutada en el Start de TimedObject
    // </summary>
    protected abstract void Initialize();
}
