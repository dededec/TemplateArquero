using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ! Cambiar a Quest en vez de Mission que se entiende mejor
public class DailyQuestsManager : TimedObject
{
    public struct Quest
    {
        // Algo que represente el objetivo a cumplir
        UnityEvent OnQuestComplete;
        bool completed;
        List<Reward> rewards;
    }

    [SerializeField] private List<Quest> _dailyQuests;
    private int _currentDay;

    protected override void Initialize()
    {
        /*
        Necesitamos cargar las misiones, y si ha pasado un día desde la última
        conexión el usuario puede hacer todas las misiones.
        */
        throw new System.NotImplementedException();
    }

    protected override void OnIntervalCompleted()
    {
        throw new System.NotImplementedException();
    }
}
