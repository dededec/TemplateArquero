using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityManager : MonoBehaviour
{
    /*
    array de booleanos que dicen si tiene x habilidad o no
    luego vemos que es cada booleano.
    */
    
    /*
    El nombre de la habilidad es para identificarlo desde el editor.
    */
    [System.Serializable]
    public class Habilidad
    {
        public string name;
        public bool _hasAbility;
    }

    [SerializeField] private List<Habilidad> _habilidades;
    private Dictionary<string, Action> _funciones;

    private void Awake() 
    {
        _funciones = new Dictionary<string, Action>();
        _funciones.Add("1", Habilidad1);

        // _funciones["1"].Invoke(); // ! Esto furula
    }

    private List<Habilidad> FindAbilities()
    {
        return _habilidades.FindAll(b => b._hasAbility == true);
    }

    public void SetAbility(int index)
    {
        _habilidades[index]._hasAbility = true;
    }

    public void SetAbility(string name)
    {
        var hab = _habilidades.Find(h => h.name == name);
        hab._hasAbility = true;
    }

    private void Habilidad1()
    {
        Debug.Log("Habilidad1");
    }
}
