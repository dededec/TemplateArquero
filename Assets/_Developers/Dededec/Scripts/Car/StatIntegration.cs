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
    // [SerializeField] private AbilityManager _abilityManager;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private TalentManager _talentManager;

    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private CarController _controller;
    [SerializeField] private CarHealthManager _healthManager;
    [SerializeField] private CarShooting _shooting;

    private float _equipmentBoost = 1f;
    private float _heroBoost = 1f;
    private int _dropsPerMinute = 0;

    /*
    Tanto TalentManager como InventoryManager dependen de Awake, así que usando Start aquí
    aseguramos que se inicialicen correctamente.
    */
    private void Start() 
    {
        _talentManager = GameObject.FindGameObjectWithTag("TalentManager").GetComponent<TalentManager>();
        _inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();

        foreach(var talent in _talentManager.Talents)
        {
            proccessTalent(talent);
        }

        foreach(var element in _inventoryManager.Equipment)
        {
            proccessEquipment(element);
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

            case "StartAbility":
            startAbilityTalent(talent.level);
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
            float amount = float.Parse(aux[1]);
            int increase = (int) (amount * _equipmentBoost);
            
            switch(aux[0].ToLower())
            {
                case "speed":
                increaseSpeed(increase);
                break;

                case "damage":
                increaseDamage(increase);
                break;
                
                default:
                break;
            }
        }
    }

    private void processHero()
    {
        
    }

    #region Talents

    private void carMaxHealthTalent(int level)
    {
        PlayerStats.instance.maxHealth += level * 10;
    }

    private void carDamageTalent(int level)
    {
        // PlayerStats.instance.IncreaseDamage(level * 10);
    }

    private void dropHealingTalent(int level)
    {
        PlayerStats.instance.dropHealing += level * 10;
    }

    private void damageReductionStillTalent(int level)
    {
        PlayerStats.instance.damageReductionStill += level * 10;
    }

    // --------------------------------------------------------

    private void damageReductionMovingTalent(int level)
    {
        PlayerStats.instance.damageReductionMoving += level * 10;
    }

    private void attackSpeedTalent(int level)
    {
        // PlayerStats.instance.IncreaseAttackSpeed(level * 10);
    }

    private void levelUpHealing(int level)
    {
        PlayerStats.instance.levelUpHealing += level * 10;
    }

    private void startAbilityTalent(int level)
    {
        if(level > 0)
        {
            AbilityManager.instance.PickNewAbility();
        }
    }

    // -------------------------------------------------------

    private void randomDropsTalent(int level)
    {
        _dropsPerMinute = level * 2;
    }

    private void EquipmentBoostTalent(int level)
    {
        /*
        En Arquero, la habilidad va de nivel 1 a 10,
        con incrementos del 3% en cada nivel (Nivel 1 te incrementa un 3%, 
        al 10 te incrementa el 30%)
        */
        _equipmentBoost += level * 0.3f;
    }

    private void HeroBoostTalent(int level)
    {
        /*
        En Arquero, la habilidad va de nivel 1 a 10,
        con incrementos del 4% en cada nivel (Nivel 1 te incrementa un 4%, 
        al 10 te incrementa el 40%)
        */
        _heroBoost += level * 0.4f;
    }

    #endregion

    #region Stat Changes

    private void increaseSpeed(int amount)
    {
        _controller.IncreaseSpeed(amount);
    }

    private void increaseDamage(int amount)
    {
        _shooting.IncreaseDamage(amount);
    }

    #endregion

    

}
