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
        ReadCSV();

        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalDays >= 1f)
        {
            // Miramos cuántos días han pasado para ver cuál le toca coger.
            CurrentDailyLoginReward += (int)timeSpan.TotalDays;
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
        _rewardManager.GiveReward(_rewards[CurrentDailyLoginReward]);
    }

    private void ReadCSV()
    {
        // Leemos CSV y guardamos cositas
        List<Dictionary<string, object>> data = CSVReader.Read(CSVFileName);

        System.DateTime currentFecha;
        System.DateTime.TryParse(data[0]["fecha"].ToString(), out currentFecha);
        List<Reward> dailyLoginRewards = new List<Reward>();

        for (int i = 0; i < data.Count; i++)
        {
            System.DateTime fecha;
            System.DateTime.TryParse(data[i]["fecha"].ToString(), out fecha);

            if (fecha != currentFecha)
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
    }
}
