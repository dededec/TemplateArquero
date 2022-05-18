using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ! Cambiar a Quest en vez de Mission que se entiende mejor
public class DailyQuestsManager : TimedObject
{
    [SerializeField] private List<Quest> _dailyQuests;
    private List<bool> _isQuestDone;
    [SerializeField] private RewardManager _rewardManager;


    protected override void Initialize()
    {
        /*
        ¿Necesitamos cargar las misiones?, y si ha pasado un día desde la última
        conexión el usuario puede hacer todas las misiones.
        */

        // ? ReadCSV();

        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalDays >= 1f)
        {
            OnIntervalCompleted();
        }
    }

    protected override void OnIntervalCompleted()
    {
        // ? ¿Cambian las misiones?

        // Se reinician las misiones.
        for (int i = 0; i < _isQuestDone.Count; ++i)
        {
            _isQuestDone[i] = false;
        }
    }

    public void QuestCompleted(int index)
    {
        if(_isQuestDone[index]) return;

        _rewardManager.GiveReward(_dailyQuests[index].rewards);
        _isQuestDone[index] = true;
    }
}
