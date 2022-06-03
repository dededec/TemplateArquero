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
    [SerializeField] private CarHealthManager _carHealthManager;
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
            case "CarMaxHealth":
            carMaxHealthTalent(talent.level);
            break;

            case "CarDamage":
            carDamageTalent(talent.level);
            break;
            
            case "DropHealing":
            dropHealingTalent(talent.level);
            break;

            case "DamageReductionStill":
            damageReductionStillTalent(talent.level);
            break;

            // --------------------------------

            case "DamageReductionMoving":
            damageReductionMovingTalent(talent.level);
            break;

            case "AttackSpeed":
            attackSpeedTalent(talent.level);
            break;

            case "LevelUpHealing":
            levelUpHealing(talent.level);
            break;

            case "StartSkill":
            startSkillTalent(talent.level);
            break;

            // ---------------------------------

            case "RandomDrops":
            randomDropsTalent(talent.level);
            break;

            case "EquipmentBoost":
            EquipmentBoostTalent(talent.level);
            break;

            case "HeroBoost":
            HeroBoostTalent(talent.level);
            break;

            // No se sabe todavía (lo mismo ni está)
            case "":
            break;

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
            int amount = int.Parse(aux[1]);
            
            switch(aux[0].ToLower())
            {
                case "speed":
                increaseSpeed(amount);
                break;

                case "damage":
                increaseDamage(amount);
                break;
                
                default:
                break;
            }
        }
    }

    #region Talents

    private void carMaxHealthTalent(int level)
    {
        _carHealthManager.maxHealth += level * 10;
    }

    private void carDamageTalent(int level)
    {
        _carShooting.IncreaseDamage(level * 10);
    }

    private void dropHealingTalent(int level)
    {
        _carHealthManager.dropHealing += level * 10;
    }

    private void damageReductionStillTalent(int level)
    {
        _carHealthManager.damageReductionStill += level * 10;
    }

    // --------------------------------------------------------

    private void damageReductionMovingTalent(int level)
    {
        _carHealthManager.damageReductionMoving += level * 10;
    }

    private void attackSpeedTalent(int level)
    {
        _carShooting.IncreaseAttackSpeed(level * 10);
    }

    private void levelUpHealing(int level)
    {
        _carHealthManager.levelUpHealing += level * 10;
    }

    private void startSkillTalent(int level)
    {
        if(level > 0)
        {
            // ! DAR HABILIDAD
        }
    }

    // -------------------------------------------------------

    private void randomDropsTalent(int level)
    {
        if(level > 0)
        {
            // ! HABILITAR DROPS
        }
    }

    private void EquipmentBoostTalent(int level)
    {
        // ! COGER CADA STAT QUE SUBE Y SUBIRLAS UN POQUITÍN
        /*
            Como ya hemos procesado el equipamiento, habría que averiguar
            cuánto subir.
        */
        // foreach(var element in _inventoryManager.Equipment)
        // {
        //     proccessEquipment(element);
        // }
    }

    private void HeroBoostTalent(int level)
    {
        // ! COGER STATS DEL HÉROE Y SUBIRLOS USANDO LEVEL.
    }

    #endregion

    #region Stat Changes

    private void increaseSpeed(int amount)
    {
        _carController.IncreaseSpeed(amount);
    }

    private void increaseDamage(int amount)
    {
        _carShooting.IncreaseDamage(amount);
    }

    #endregion

    

}
