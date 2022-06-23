using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private static PlayerStats _instance;
    public static PlayerStats instance
    {
        get 
        {
            if(_instance == null)
                _instance = new PlayerStats();
            return _instance;
        }
    }

    // Movement
    public float accelerationFactor  = 25f;
    public float maxSpeed            = 20f;

    // Combat
    public float attackSpeed         = 0.5f;
    public int attackDamage          = 5;
    
    // Health and Defense
    public int maxHealth             = 100;
    public int currentHealth         = 100;

    public int damageReductionStill  = 0;
    public int damageReductionMoving = 0;
    public int dropHealing           = 0;
    public int levelUpHealing        = 0;

    private PlayerStats()
    {

    }

    public void ResetStats()
    {
        accelerationFactor    = 25f;
        maxSpeed              = 20f;

        attackSpeed           = 0.5f;
        attackDamage          = 5;

        maxHealth             = 100;
        currentHealth         = 100;
        
        damageReductionStill  = 0;
        damageReductionMoving = 0;
        dropHealing           = 0;
        levelUpHealing        = 0;
        
        Debug.Log("Stats Reseteadas");
    }

    public void AssignHeroStats(Hero hero)
    {
        accelerationFactor    = hero.AccelerationFactor;
        maxSpeed              = hero.MaxSpeed;

        attackSpeed           = hero.AttackSpeed;
        attackDamage          = hero.AttackDamage;

        maxHealth             = hero.MaxHealth;
        currentHealth         = hero.MaxHealth;
        
        damageReductionStill  = hero.DamageReductionStill;
        damageReductionMoving = hero.DamageReductionMoving;
        dropHealing           = hero.DropHealing;
        levelUpHealing        = hero.LevelUpHealing;
    }
}
