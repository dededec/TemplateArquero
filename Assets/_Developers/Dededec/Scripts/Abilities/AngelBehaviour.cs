using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AngelBehaviour : MonoBehaviour
{
    [SerializeField] private AbilityManager _playerAbilities;

    // Start is called before the first frame update
    void Start()
    {
        _playerAbilities = GameObject.FindGameObjectWithTag("Car").GetComponent<AbilityManager>();
        if(_playerAbilities == null)
        {
            Debug.LogError("AbilityManager no encontrado.");
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Car")
        {
            _playerAbilities.PickNewAbility();
        }
    }
}
