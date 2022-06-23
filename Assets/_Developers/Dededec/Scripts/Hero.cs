using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private static Dictionary<string, float> _nameMultiplier;
    

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

    [SerializeField] private float _multiplier = 1;

    private void Awake() 
    {
        /*
        Se necesita acceder al multiplier, pero este no es persistente, por lo que
        hay que guardarlo en el SaveData y saber a cuál acceder.

        Unity -> static Dictionary<string, float> NombreMultiplier.
        SaveData -> string[] y float[], donde float[i] es el multiplier de string[i]
        */   
        if(_nameMultiplier == null)
        {
            _nameMultiplier = new Dictionary<string, float>();
            for(int i=0; i<SaveDataController.HeroNames.Length; ++i)
            {
                _nameMultiplier.Add(SaveDataController.HeroNames[i], SaveDataController.HeroMultipliers[i]);
            }
        }
    }

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
