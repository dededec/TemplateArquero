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
    public float accelerationFactor = 30f;
    public float maxSpeed = 20f;

    // Combat
    public float attackSpeed;
    public int attackDamage;
    
    // Health and Defense
    public int maxHealth = 100;
    public int damageReductionStill;
    public int damageReductionMoving;
    public int dropHealing;
    public int levelUpHealing;
    public int currentHealth = 100;
}
