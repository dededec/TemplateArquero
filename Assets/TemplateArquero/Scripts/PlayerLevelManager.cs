using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelManager : MonoBehaviour
{
    private const string _levelDataFileName = "LevelData.csv";
    private const string _rewardDataFileName = "LevelRewards.csv";


    [Tooltip("Total experience needed to reach player level at position i.")]
    [SerializeField] private List<int> _experienceToReach;

    [Tooltip("Rewards awarded when reaching player level at position i.")]
    List<List<Reward>> _rewardsPerLevel;
    [SerializeField] RewardManager _rewardManager;

    private int _lastLevelReached;

    public int Experience
    {
        get
        {
            return SaveDataController.Experience;
        }

        set
        {
            SaveDataController.Experience = value;
        }
    }

    private void Start()
    {
        var path = Application.persistentDataPath + "/" + _levelDataFileName;
        using (var reader = new StreamReader(path))
        {
            // Leemos la primera linea donde estan los titulos.
            var line = reader.ReadLine();
            _experienceToReach = new List<int>();
            
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine(); // La línea será del estilo ValorA
                _experienceToReach.Add(Int32.Parse(line));
            }
        }

        path = Application.persistentDataPath + "/" + _rewardDataFileName;
        using (var reader = new StreamReader(path))
        {
            // Leemos la primera linea donde estan los titulos.
            var line = reader.ReadLine();
            _experienceToReach = new List<int>();
            
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine(); // La línea será del estilo ValorA;ValorB;ValorC;ValorD
                var values = line.Split(';');

                // ! Añadir rewards a _rewardsPerLevel
            }
        }
    }

    public void AddExperience(int amount)
    {
        if (amount < 0) return;

        Experience += amount;

        int aux = _lastLevelReached;
        if (_experienceToReach.Count > _lastLevelReached + 1)
        {
            for (int i = _lastLevelReached; i < _experienceToReach.Count; ++i)
            {
                if (Experience < _experienceToReach[i])
                {
                    break;
                }

                _lastLevelReached = i;
            }
        }

        for (int i = aux; i <= _lastLevelReached; ++i)
        {
            _rewardManager.GiveReward(_rewardsPerLevel[i]);
        }

        return;
    }
}
