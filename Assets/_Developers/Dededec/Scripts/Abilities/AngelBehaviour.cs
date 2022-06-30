using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AngelBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(!enabled) return;
        
        if(other.gameObject.tag == "Player")
        {
            AbilityManager.instance.PickNewAbility();
            this.enabled = false;
        }
    }
}
