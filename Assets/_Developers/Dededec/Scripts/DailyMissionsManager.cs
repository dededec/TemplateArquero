using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyMissionsManager : TimedObject
{
    public struct Mission
    {
        // Algo que represente el objetivo a cumplir
        UnityEvent OnMissionComplete;
        bool completed;
        List<Reward> rewards;
    }

    [SerializeField] private List<Mission> _dailyMissions;
    private int _currentDay;

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnIntervalCompleted()
    {
        throw new System.NotImplementedException();
    }
}
