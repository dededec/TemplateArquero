using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementManager : MonoBehaviour
{
    /*
    No es un TimedObject.
    Esto se debería leer un CSV o algo y de ahí se sacan los
    achievements.
    Luego deberíamos tener guardados cuales están hechos,
    que sería un array de booleanos.
    */

    private const string CSVFileName = "Achievements";

    [SerializeField] private RewardManager _rewardManager;

    [SerializeField] private List<Quest> _achievements;
    private string[] achievementData = null;
    string achievements = SaveDataController.Achievements;

    private void Start() 
    {
        ReadCSV();
        achievementData = achievements.Split(";");
        loadData();
    }

    private void ReadCSV()
    {
        // Leemos CSV y guardamos cositas
        List<Dictionary<string, object>> data = CSVReader.Read(CSVFileName);

        // System.DateTime currentFecha;
        // System.DateTime.TryParse(data[0]["fecha"].ToString(), out currentFecha);
        // List<DailyQuestsManager.Quest> dailyLoginRewards = new List<DailyQuestsManager.Quest>();

        // for (int i = 0; i < data.Count; i++)
        // {
        //     System.DateTime fecha;
        //     System.DateTime.TryParse(data[i]["fecha"].ToString(), out fecha);

        //     if (fecha != currentFecha)
        //     {
        //         _achievements.Add(dailyLoginRewards);
        //         currentFecha = fecha;
        //         dailyLoginRewards.Clear();
        //     }

        //     string id = data[i]["id"].ToString();

        //     int quantity;
        //     int.TryParse(data[i]["quantity"].ToString(), out quantity);

        //     dailyLoginRewards.Add(new Reward(id, quantity));
        // }
    }

    public void GiveReward(int index)
    {
        _rewardManager.GiveReward(_achievements[index].rewards);
        foreach(string a in achievementData)
        {
            if(a.Split("-")[0] == _achievements[index].id)
            {
                a.Split("-")[1] = "100";
            }
        }
        saveData();
        // isAchievementCompleted[index] = true;
        
        // if(!find(isAchievementCompleted, false))
        // {
        //     Debug.LogWarning("No quedan más logros que conseguir");
        // }
         
        // ? Si no hay más achievements, qué pasa? Se crean nuevos supongo.
    }

    public void updateProgress(int index, int progress)
    {
        foreach(string a in achievementData)
        {
            if(a.Split("-")[0] == _achievements[index].id)
            {
                a.Split("-")[1] = "" + _achievements[index].AddProgress(progress);

            }
        }
    }

    private void loadData()
    {
        int index = 0;
        foreach(string a in achievementData)
        {
            if(a.Split("-")[0] == _achievements[index].id)
            {
                _achievements[index].progress = int.Parse(a.Split("-")[1]);
            }
            index++;
        }
    }

    private void saveData()
    {
        string aux = "";
        foreach(string s in achievementData)
        {
            aux += s + ";";
        }

        SaveDataController.Achievements = aux;
    }

    private bool find(bool[] array, bool value)
    {
        for(int i=0, j=array.Length-1; i <= j; ++i, ++j)
        {
            if(array[i] == value || array[j] == value)
            {
                return true;
            }
        }

        return false;
    }

}
