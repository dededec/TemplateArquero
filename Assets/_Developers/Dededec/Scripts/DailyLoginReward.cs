using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyLoginReward : TimedObject
{
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private List<List<Reward>> _rewards;
    private int _currentDay;
    [SerializeField] private TimeManager _timeManager;

    private bool _canGetReward = false;

    /*
    Hay que hacer igual que con la energía, pero en este caso hay que ver
    que si ha pasado más de un día desde la última vez se pasa a la siguiente
    recompensa.
    */

    private void OnEnable()
    {
        /*
        Miramos si ha cambiado de día, y si ha cambiado pos hay nueva cosa de esa que coges.
        */

        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();

        if(timeSpan.TotalDays >= 1f) // Ha pasado al menos 1 día.
        {
            // Miramos cuántos días han pasado para ver cuál le toca coger.
            // ! Las recompensas del día _currentDay a _currentDay+TotalDays no se pueden coger.
            _currentDay += (int) timeSpan.TotalDays;
        }
    }

    protected override void OnIntervalCompleted()
    {
        Debug.Log("SFSDFSDF");
    }

    public void GetDailyReward()
    {
        _rewardManager.GiveReward(_rewards[_currentDay]);
    }
}
