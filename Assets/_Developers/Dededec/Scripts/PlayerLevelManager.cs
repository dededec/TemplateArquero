using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelManager : MonoBehaviour
{
    public static UnityEvent onNewLevel;

    [Tooltip("Total experience needed to reach player level at position i.")]
    [SerializeField] private List<int> _experienceToReach;

    // List<List<Reward>> rewardsPerLevel;
    
    // !!!! La experiencia y los rewards se leen desde un archivo

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
            onNewLevel.Invoke();
        }

        return;
    }
}
