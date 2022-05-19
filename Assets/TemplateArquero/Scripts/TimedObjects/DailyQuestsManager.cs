using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyQuestsManager : TimedObject
{
    [SerializeField] private List<Quest> _dailyQuests;
    private List<bool> _isQuestDone;
    [SerializeField] private RewardManager _rewardManager;
    private string[] questData = null;

    public string DailyQuests
    {
        get
        {
            return SaveDataController.DailyQuests;
        }

        set
        {
            SaveDataController.DailyQuests = value;
        }
    }


    protected override void Initialize()
    {
        /*
        ¿Necesitamos cargar las misiones?, y si ha pasado un día desde la última
        conexión el usuario puede hacer todas las misiones.
        */

        // ? ReadCSV();
        questData = DailyQuests.Split(";");
        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalDays >= 1f)
        {
            OnIntervalCompleted();
        }
    }

    protected override void OnIntervalCompleted()
    {
        // ? ¿Cambian las misiones?

        foreach(var quest in _dailyQuests)
        {
            quest.progress = 0;
        }
    }

    public void SetProgress(string id, int progress)
    {
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

    public void QuestCompleted(string id)
    {
        Quest quest = _dailyQuests.Find(quest => quest.id == id);
        if(quest == null)
        {
            Debug.LogError("Error (QuestCompleted): Quest con id ( " + id + " ) no encontrada.");
            return;
        }
        
        QuestCompleted(quest);
    }

    public void QuestCompleted(Quest quest)
    {
        _rewardManager.GiveReward(quest.rewards);
    }
}
