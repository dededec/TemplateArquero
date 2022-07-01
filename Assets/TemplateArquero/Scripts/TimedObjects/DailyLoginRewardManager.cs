using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyLoginRewardManager : TimedObject
{
    private const string CSVFileName = "DailyLoginRewards";

    [Header("Reward Settings")]
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private DailyQuestsManager _questManager;
    [SerializeField] private List<List<Reward>> _rewards = new List<List<Reward>>();
    private bool _isRewardGiven = false;
    
    private int CurrentDailyLoginReward
    {
        get
        {
            return SaveDataController.CurrentDailyLoginReward;
        }

        set
        {
            SaveDataController.CurrentDailyLoginReward = value;
        }
    }

    protected override void Initialize()
    {

        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();

        if (timeSpan.TotalDays >= 1f)
        {
            for(int i=0; i<timeSpan.TotalDays; ++i)
            {
                OnIntervalCompleted();
            }
        }
    }

    protected override void OnIntervalCompleted()
    {
        Debug.Log("Se ha completado un intervalo, hay nueva recompensa.");
        CurrentDailyLoginReward++;
        if(CurrentDailyLoginReward >= _rewards.Count)
        {
            // Has pasado todas los dias de los rewards.
            // (Si se reincia tal cual) empezamos desde el principio
            CurrentDailyLoginReward = 0;
        }
    }

    public void GetDailyReward()
    {
        if(_isRewardGiven) return;

        _rewardManager.GiveReward(_rewards[CurrentDailyLoginReward]);
        _isRewardGiven = true;
    }
}
