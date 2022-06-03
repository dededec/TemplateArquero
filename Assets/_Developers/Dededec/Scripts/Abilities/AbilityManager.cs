using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityManager : MonoBehaviour
{    
    [Serializable]
    public class AbilityElement
    {
        public Ability _ability;
        public bool _hasAbility;

        public AbilityElement()
        {
            _ability = null;
            _hasAbility = false;
        }

        public AbilityElement(Ability ability, bool hasAbility)
        {
            _ability = ability;
            _hasAbility = hasAbility;
        }
    }

    [Header("Shooting Abilities")]
    /*
    Indice - Función:
    0 - TripleShot
    1 - 
    2 - 
    */
    [SerializeField] private List<AbilityElement> _abilities;
    [SerializeField] private CarShooting _shooting;

    private void Awake() 
    {
        for(int i=0; i < 20; ++i)
        {
            _abilities.Add(new AbilityElement());
        }
    }

    public void Shoot()
    {
        for(int i = 0; i< _abilities.Count; ++i)
        {
            if(_abilities[i]._hasAbility)
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

    #region Auxiliary Methods

    public List<AbilityElement> FindAbilities(bool value)
    {
        return _abilities.FindAll(b => b._hasAbility == value);
    }

    public List<int> FindAbilitiesIndex(bool value)
    {
        List<int> result = new List<int>();
        for(int i=0; i < _abilities.Count; ++i)
        {
            if(_abilities[i]._hasAbility == value)
            {
                result.Add(i);
            }
        }

        return result;
    }

    public void SetAbility(int index)
    {
        _abilities[index]._hasAbility = true;
    }

    public void PickNewAbility()
    {
        var abilities = SublistAbilities();
        /*
        Habría que hacer una UI y asignar las cosas de cada habilidad.
        Cada habilidad en la UI es un botón, que al pulsarse te da la 
        habilidad pulsada.
        */
    }

    public List<AbilityElement> SublistAbilities()
    {
        var indexes = FindAbilitiesIndex(false);
        List<AbilityElement> result = new List<AbilityElement>();
        int[] abilities;

        // Elegimos tres indices al azar.
        if(indexes.Count >= 3)
        {
            abilities = new int[3];
            for(int i=0; i<3; ++i)
            {
                int random = -1;
                do
                {
                    random = UnityEngine.Random.Range(0, indexes.Count);
                }while(find(abilities, random));
                
                abilities[i] = random;
                result.Add(_abilities[i]);
            }
        }
        else if(indexes.Count > 0)
        {
            abilities = new int[indexes.Count];
            for(int i=0; i<indexes.Count; ++i)
            {
                abilities[i] = indexes[i];
                result.Add(_abilities[i]);
            }
        }

        return result;
    }

    public bool find(int[] array, int target) 
    {
        return Array.Exists(array, x => x == target);
    }

    #endregion

    #region Abilities

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

    #endregion
}
