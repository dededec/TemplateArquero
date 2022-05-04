using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecharge : MonoBehaviour
{
    [Tooltip("Time left in seconds until energy recharges.")]
    [SerializeField] private float _timeLeft;

    [Tooltip("Time in seconds until energy recharges.")]
    [SerializeField] private float _timeToRecharge;

    [Tooltip("Amount of energy added when TimeToRecharge seconds passes.")]
    [SerializeField] private int _energyRecharged;

    [Tooltip("Maximum energy obtained passively.")]
    [SerializeField] private int _maxEnergy;

    // ? Tiene sentido?
    [SerializeField] private TimeManager _timeManager;

    public float TimeLeft
    {
        get
        {
            return _timeLeft;
        }
    }

    private void OnEnable() 
    {
        // Miramos cuantas veces se ha completado la recarga mientras no se contaba.
        int timeLoops = (int) (_timeManager.TimeSinceLastConnection().TotalSeconds / _timeToRecharge);
        float resto = ((float)_timeManager.TimeSinceLastConnection().TotalSeconds) % _timeToRecharge;

        for(int i=0; i < timeLoops; ++i)
        {
            AddEnergy();
        }

        _timeLeft = _timeToRecharge - resto;
    }

    void Update()
    {
        if(_timeLeft > 0f)
        {
            _timeLeft -= Time.deltaTime;
        }
        else
        {
            AddEnergy();
            _timeLeft = _timeToRecharge;
        }
    }

    private void AddEnergy()
    {
        if(EconomyManager.Energy < _maxEnergy)
        {
            if(EconomyManager.Energy + _energyRecharged > _maxEnergy)
            {
                // Llenamos la energía al máximo sin pasarnos del límite
                EconomyManager.Add(EconomyManager.CoinType.ENERGY, _maxEnergy - EconomyManager.Energy);
            }
            else
            {
                EconomyManager.Add(EconomyManager.CoinType.ENERGY, _energyRecharged);
            }

            Debug.Log("Energia: " + EconomyManager.Energy);
        }
    }
}
