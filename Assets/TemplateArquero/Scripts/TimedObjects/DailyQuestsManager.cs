using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyQuestsManager : TimedObject
{
    [SerializeField] private List<Quest> _dailyQuests = new List<Quest>();
    [SerializeField] private RewardManager _rewardManager;

    public bool[] CompletedDailyQuests
    {
        get
        {
            return SaveDataController.CompletedDailyQuests;
        }

        set
        {
            SaveDataController.CompletedDailyQuests = value;
        }
    }

    public bool[] ReclaimedDailyQuests
    {
        get
        {
            return SaveDataController.ReclaimedDailyQuests;
        }

        set
        {
            SaveDataController.ReclaimedDailyQuests = value;
        }
    }

    #region Protected Methods

    protected override void Initialize()
    {
        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalDays >= 1f)
        {
            OnIntervalCompleted();
        }
    }

    protected override void OnIntervalCompleted()
    {
        for(int i=0; i < _dailyQuests.Count; ++i)
        {
            _dailyQuests[i].progress = 0;
            CompletedDailyQuests[i] = false;
            ReclaimedDailyQuests[i] = false;
        }
    }

    #endregion

    #region Quest control Methods

    public void SetProgress(string id, int progress)
    {
        Debug.Log("id: " + id + " DailyQuest: " + (_dailyQuests.Count));
        Quest quest = _dailyQuests.Find(q => q.id == id);
        if(quest != null)
        {
            quest.progress = progress;
            if(progress >= 100)
            {
                QuestCompleted(quest);
            }
        }
    }

    public void QuestCompleted(Quest quest)
    {   
        int index = _dailyQuests.FindIndex(q => q == quest);
        if(CompletedDailyQuests[index]) return;

        CompletedDailyQuests[index] = true;
    }

    public void QuestReclaimed(Quest quest)
    {
        int index = _dailyQuests.FindIndex(q => q == quest);
        if(!CompletedDailyQuests[index] || ReclaimedDailyQuests[index]) return;

        ReclaimedDailyQuests[index] = true;
        _rewardManager.GiveReward(quest.rewards);
    }

    // public void ProgressQuest(string id)
    // {
        // switch(id)
        // {
        //     case "BuyOrWatchVideo":
        //     case "CarUpgrade":
        //     case "EquipmentUpgrade":
        //     case "EquipmentFuse":
        //     case "GoldChestOpen":
        //     case "Login":
        //     case "ObsidianChestOpen":
        //     SetProgress(id, 100);
        //     break;
            
        //     case "DefeatEnemies":
        //     _enemiesDefeated++;
        //     if(_enemiesDefeated >= 3)
        //     {
        //         SetProgress("DefeatEnemies", Lerp(0, 100, _enemiesDefeated/3));
        //     }
        //     break;

        //     case "PlayedLevels":
        //     _levelsPlayed++;
        //     if(_levelsPlayed > 2)
        //     {
        //         SetProgress("PlayedLevels", Lerp(0, 100, _enemiesDefeated/2));
        //     }
        //     break;
        // }
    // }

    #endregion


    private int Lerp(int a, int b, float t) => (int) Mathf.Lerp(a,b,t);

}
