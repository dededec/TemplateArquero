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

    #region Singleton

    private static AbilityManager _instance;
    public static AbilityManager instance
    {
        get 
        {
            if(_instance == null)
                _instance = new AbilityManager();
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _abilityPickUI = GameObject.FindGameObjectWithTag("AbilityUI");
        _abilityPickUI.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            _buttons[i] = _abilityPickUI.transform.GetChild(1).GetChild(i).GetComponent<Button>();
            _icons[i] = _abilityPickUI.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>();
            _names[i] = _abilityPickUI.transform.GetChild(1).GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();
            _descriptions[i] = _abilityPickUI.transform.GetChild(1).GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>();
        }
    }

    #endregion

    /*
    Orden de habilidades:
    [x, y].- Modificadores de disparo
    [x, y].- Modificadores de impacto (rebote en pared, en enemigos, etc)
    [x, y].- Habilidades elementales de disparo (balas de veneno y cosas así)
    [x, y].- Habilidades elementales de estela de drift 
    [x, y].- Habilidades que solo se llaman una vez al conseguirse (aumento de estadísticas, heal, etc)
    [x, y].- Mejoras de stats si se limpia una sala sin recibir daño

    -------------------------
    */

    [SerializeField] private List<AbilityElement> _abilities;

    [Header("Pick Ability UI")]
    [SerializeField] private GameObject _abilityPickUI;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _icons;
    [SerializeField] private TMP_Text[] _names;
    [SerializeField] private TMP_Text[] _descriptions;

    /*
    Si se stackean habilidades:
    
    */

    #region Properties

    public static List<AbilityElement> Abilities
    {
        get
        {
            return instance._abilities;
        }

        set
        {
            instance._abilities = value;
        }
    }
    
    #endregion

    #region Public Methods

    public void PickNewAbility()
    {
        // Parar control del jugador
        GameStateManager.instance.SetState(GameState.Paused);

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

        // Devolver control
        GameStateManager.instance.SetState(GameState.Gameplay);
    }

    public void UseAbilities(int inicio, int fin, EnemyBase target = null)
    {
        for(int i = inicio; i < fin; ++i)
        {
            if(_abilities[i].hasAbility)
            {
                UseAbility(i, target);
            }
        }
    }

    public void ShootAbilities() => UseAbilities(0, 8);
    
    public void OnHitAbilities(EnemyBase target)
    {
        UseAbilities(8, 10, target);
    }

    public bool HasAbility(string name)
    { 
        var ab = _abilities.Find(ab => ab.ability.name == name);
        if(ab == null)
        {
            Debug.Log("Habilidad no encontrada, nombre: " + name);
            return false;
        }
        else
        {
            return ab.hasAbility;
        }
    }

    public bool HasAbility(int index) => _abilities[index].hasAbility;        
    
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
 
    private void UseAbility(int index, EnemyBase target = null)
    {
        switch(index)
        {
            // case 0:
            // TripleShot();
            // break;

            // case 1: break; ... case 8: break;
    
            // case 9:
            // ApplyPoison(target);
            // break;
            // case 10:
            // ApplyBurn(target);
            // break;

            default:
            Debug.LogWarning("Indice de habilidad " + index + " no tiene habilidad asignada.");
            break;
        }
    }

    #endregion

    #region Abilities Example

    // #region Shot Modifiers

    // private void TripleShot()
    // {
    //     // _shooting.Shoot(_shooting.transform.position + _shooting.transform.right + _shooting.transform.forward);
    //     // _shooting.Shoot(_shooting.transform.position - _shooting.transform.right + _shooting.transform.forward);
    // }

    // #endregion

    // #region OnHitAbilities

    // private void ApplyPoison(EnemyBase enemy)
    // {
    //     enemy.ApplyPoison();
    // }

    // private void ApplyBurn(EnemyBase enemy)
    // {
    //     enemy.ApplyBurn();
    // }

    // #endregion

    #endregion
}
