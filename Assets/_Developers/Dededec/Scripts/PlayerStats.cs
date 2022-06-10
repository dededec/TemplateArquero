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

    private PlayerStats()
    {

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
}
