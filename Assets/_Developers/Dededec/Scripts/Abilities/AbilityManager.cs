using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class AbilityManager : MonoBehaviour
{    
    [Serializable]
    public class AbilityElement
    {
        public Ability ability;
        public bool hasAbility;

        public AbilityElement()
        {
            ability = null;
            hasAbility = false;
        }

        public AbilityElement(Ability ability, bool hasAbility)
        {
            this.ability = ability;
            this.hasAbility = hasAbility;
        }
    }

    /*
    Indice - Función:
    0 - TripleShot
    1 - 
    2 - 
    */
    [SerializeField] private List<AbilityElement> _abilities;
    [SerializeField] private CarShooting _shooting;
    [SerializeField] private CarController _movement;

    [Header("Pick Ability UI")]
    [SerializeField] private GameObject _abilityPickUI;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _icons;
    [SerializeField] private TMP_Text[] _names;
    [SerializeField] private TMP_Text[] _descriptions;

    public void Shoot()
    {
        for(int i = 0; i< _abilities.Count; ++i)
        {
            if(_abilities[i].hasAbility)
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
            Debug.LogWarning("Indice de habilidad " + index + " no tiene habilidad asignada.");
            break;
        }
    }

    #region Public Methods

    public void PickNewAbility()
    {
        List<AbilityElement> abilitiesToChoose = SublistAbilities();

        _abilityPickUI.SetActive(true);
        for(int i=0; i < abilitiesToChoose.Count; ++i)
        {
            _icons[i].sprite = abilitiesToChoose[i].ability.icon;
            _names[i].text = abilitiesToChoose[i].ability.name;
            _descriptions[i].text = abilitiesToChoose[i].ability.description;

            var aux = i;
            _buttons[i].onClick.RemoveAllListeners();
            _buttons[i].onClick.AddListener(delegate() { SetAbility(abilitiesToChoose[aux]); });
        }
    }

    public void SetAbility(AbilityElement element)
    {
        if(_abilityPickUI.activeSelf)
        {
            _abilityPickUI.SetActive(false);
        }
        element.hasAbility = true;
    }

    public List<AbilityElement> SublistAbilities()
    {
        var indexes = FindAbilitiesIndex(false);
        List<AbilityElement> result = new List<AbilityElement>();
        int[] abilities;

        // Elegimos tres indices al azar.
        if(indexes.Count > 3)
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
                result.Add(_abilities[random]);
            }
        }
        else if(indexes.Count > 0)
        {
            abilities = new int[indexes.Count];
            for(int i=0; i<indexes.Count; ++i)
            {
                abilities[i] = indexes[i];
                result.Add(_abilities[indexes[i]]);
            }
        }

        return result;
    }
    
    #endregion

    #region Private Methods

    private bool find(int[] array, int target) 
    {
        return Array.Exists(array, x => x == target);
    }

    private List<int> FindAbilitiesIndex(bool value)
    {
        List<int> result = new List<int>();
        for(int i=0; i < _abilities.Count; ++i)
        {
            if(_abilities[i].hasAbility == value)
            {
                result.Add(i);
            }
        }

        return result;
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
