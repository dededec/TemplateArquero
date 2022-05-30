using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityManager : MonoBehaviour
{    

    [Header("Shooting Abilities")]
    /*
    Indice - Función:
    0 - TripleShot
    1 - 
    2 - 
    */
    [SerializeField] private List<bool> _hasShotAbility;
    [SerializeField] private CarShooting _shooting;

    private int _shotAbilityNumber = 20;


    private void Awake() 
    {
        for(int i=0; i<_shotAbilityNumber; ++i)
        {
            _hasShotAbility.Add(false);
        }
    }

    public void Shoot()
    {
        for(int i = 0; i< _hasShotAbility.Count; ++i)
        {
            if(_hasShotAbility[i])
            {
                UseAbility(i);
            }
        }
    }

    private void UseAbility(int index)
    {
        switch(index)
        {
            case 0:
            TripleShot();
            break;
            case 1:
            LateralShot();
            break;
            default:
            Debug.LogError("Indice de habilidad " + index + " no válido.");
            break;
        }
    }

    private List<bool> FindAbilities()
    {
        return _hasShotAbility.FindAll(b => b == true);
    }

    public void SetAbility(int index)
    {
        _hasShotAbility[index] = true;
    }

    private void TripleShot()
    {
        _shooting.Shoot(_shooting.transform.position + 3 * _shooting.transform.forward, _shooting.transform.rotation);
        _shooting.Shoot(_shooting.transform.position + 2 * _shooting.transform.forward, _shooting.transform.rotation);
    }

    private void LateralShot()
    {
        _shooting.Shoot(_shooting.transform.position + _shooting.transform.right + _shooting.transform.forward, _shooting.transform.rotation);
        _shooting.Shoot(_shooting.transform.position - _shooting.transform.right + _shooting.transform.forward, _shooting.transform.rotation);
    }
}
