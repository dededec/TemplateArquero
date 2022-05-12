using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyLoginRewardManager : TimedObject
{
    private const string CSVFileName = "DailyLoginRewards";

    [Header("Reward Settings")]
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private List<List<Reward>> _rewards;
    private int _currentDay;

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // !!!!! LO DEL CSV PA CARGAR LOS REWARDS
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    /*
        Hay que hacer igual que con la energía, pero en este caso hay que ver
        que si ha pasado más de un día desde la última vez se pasa a la siguiente
        recompensa.
    */
    protected override void Initialize()
    {
        // Leemos CSV y guardamos cositas
        List<Dictionary<string, object>> data = CSVReader.Read(CSVFileName);

        System.DateTime currentFecha;
        System.DateTime.TryParse(data[0]["fecha"].ToString(), out currentFecha);
        List<Reward> dailyLoginRewards = new List<Reward>();

        for(int i = 0; i < data.Count; i++)
        {
            System.DateTime fecha;
            System.DateTime.TryParse(data[i]["fecha"].ToString(), out fecha);

            if(fecha != currentFecha)
            {
                _rewards.Add(dailyLoginRewards);
                currentFecha = fecha;
                dailyLoginRewards.Clear();
            }
            
            string id = data[i]["id"].ToString();
            
            int quantity;
            int.TryParse(data[i]["quantity"].ToString(), out quantity);

            dailyLoginRewards.Add(new Reward(id, quantity));
        }

        /*
        Miramos si ha cambiado de día, y si ha cambiado pos hay nueva cosa de esa que coges.
        */
        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalDays >= 1f) // Ha pasado al menos 1 día.
        {
            // Miramos cuántos días han pasado para ver cuál le toca coger.
            // ! Las recompensas del día _currentDay a _currentDay+TotalDays no se pueden coger.
            _currentDay += (int)timeSpan.TotalDays;
        }
    }

    protected override void OnIntervalCompleted()
    {
        Debug.Log("Se ha completado un intervalo, hay nueva recompensa.");
        // Se pasa de recompensa (solo se puede coger la de currentday).
        _currentDay++;
    }

    public void GetDailyReward()
    {
        _rewardManager.GiveReward(_rewards[_currentDay]);
    }
}
