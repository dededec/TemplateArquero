using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatIntegration : MonoBehaviour
{
    /*
    Aquí cogemos equipamiento, talentos y demás (no sé si hay demás) y 
    los aplicamos al coche (es decir, si el equipamiento te da +3 velocidad, pos te lo aplica).
    */
    
    [Header("Managers")]
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private TalentManager _talentManager;

    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private CarController _carController;
    [SerializeField] private CarShooting _carShooting;

    
    private void Awake() 
    {
        _talentManager = GameObject.FindGameObjectWithTag("TalentManager").GetComponent<TalentManager>();
        _inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();

        foreach(var element in _inventoryManager.Equipment)
        {
            proccessEquipment(element);
        }

        foreach(var talent in _talentManager.Talents)
        {
            proccessTalent(talent);
        }
    }

    private void proccessTalent(TalentManager.Talent talent)
    {
        switch(talent.name)
        {
            /*
            case "NOMBRE DEL TALENTO":
            funcionTalento(talent.level);
            break;
            */

            default:
            Debug.Log(talent.name);
            break;
        }
    }

    private void proccessEquipment(Item item)
    {
        // Formato JSON: Stat:valor;stat:valor;
        string stats = item.stats;
        stats.Replace(" ", String.Empty);
        Debug.Log("Item: " + item.name + " - Stats: " + stats);
        string[] statsSplit = stats.Split(";");
        
        foreach(var stat in statsSplit)
        {
            // string con formato stat:valor
            string[] aux = stat.Split(":");
            
            switch(aux[0])
            {
                /*
                case "NOMBRESTAT":
                Apply$NOMBRESTAT(aux[1]);
                break;
                */
                case "Speed":
                IncreaseSpeed(int.Parse(aux[1]));
                break;
                
                default:
                break;
            }
        }
    }


    #region Stat Changes

    private void IncreaseSpeed(int amount)
    {
        
    }

    #endregion

}
