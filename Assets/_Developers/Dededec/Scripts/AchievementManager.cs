using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private List<DailyQuestsManager.Quest> _achievements;

    // private bool[] isAchievementCompleted
    // {
    //     get
    //     {
    //         return SaveDataController.CurrentDailyLoginReward;
    //     }

    //     set
    //     {
    //         SaveDataController.CurrentDailyLoginReward = value;
    //     }
    // }

    private void Start() 
    {
        ReadCSV();
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

}
