using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    // Movement
    [SerializeField] private float _accelerationFactor  = 25f;
    [SerializeField] private float _maxSpeed            = 20f;

    // Combat
    [SerializeField] private float _attackSpeed         = 0.5f;
    [SerializeField] private int _attackDamage          = 5;
    
    // Health and Defense
    [SerializeField] private int _maxHealth             = 100;

    [SerializeField] private int _damageReductionStill  = 0;
    [SerializeField] private int _damageReductionMoving = 0;
    [SerializeField] private int _dropHealing           = 0;
    [SerializeField] private int _levelUpHealing        = 0;

    public float AccelerationFactor { get => _accelerationFactor; private set => _accelerationFactor = value; }
    public float MaxSpeed { get => _maxSpeed; private set => _maxSpeed = value; }
    public float AttackSpeed { get => _attackSpeed; private set => _attackSpeed = value; }
    public int AttackDamage { get => _attackDamage; private set => _attackDamage = value; }
    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
    public int DamageReductionStill { get => _damageReductionStill; private set => _damageReductionStill = value; }
    public int DamageReductionMoving { get => _damageReductionMoving; private set => _damageReductionMoving = value; }
    public int DropHealing { get => _dropHealing; private set => _dropHealing = value; }
    public int LevelUpHealing { get => _levelUpHealing; private set => _levelUpHealing = value; }
}
