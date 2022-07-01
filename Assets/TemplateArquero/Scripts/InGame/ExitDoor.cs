using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnEnable() 
    {
        Debug.Log("ExitDoor activada");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!enabled) return;

        if(other.gameObject.tag == "Player")
        {
            WorldManager.AdvanceStage();
        }    
    }
}
