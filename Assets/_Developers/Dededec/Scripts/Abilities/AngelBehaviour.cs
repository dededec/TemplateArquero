using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AngelBehaviour : MonoBehaviour
{
    // [SerializeField] private AbilityManager _playerAbilities;

    // Start is called before the first frame update
    void Start()
    {
        // _playerAbilities = GameObject.FindGameObjectWithTag("Car").GetComponent<AbilityManager>();
        // _playerAbilities = AbilityManager.instance;
        // if(_playerAbilities == null)
        // {
        //     Debug.LogError("AbilityManager no encontrado.");
        // }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!enabled) return;
        
        if(other.gameObject.tag == "Car")
        {
            Debug.Log("NUEVA HABILIDAD MAMAHUEVASO");
            AbilityManager.instance.PickNewAbility();
            this.enabled = false;
        }
    }
}
