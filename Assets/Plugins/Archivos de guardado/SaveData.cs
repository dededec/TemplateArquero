using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // Currency
    public int softCoins;
    public int hardCoins;
    public int energy;
    public int experience;

    // Last Time Connected: dd/mm/yyyy hh/mm/ss
    public string lastConnection;

    // Array of talent
    public int[] talentsList;
    public string inventory;
    public string equipment;
    public string equippedHero;

    public float[] heroMultipliers;

    // Progress
    public int currentWorld;
    public int[] highestStageReached;

    // Daily Login Reward
    public int currentDailyLoginReward;

    public string achievements;
    public bool[] completedDailyQuests;
    public bool[] reclaimedDailyQuests;

    // public bool boolExample;
    // public int intExample;
    // public string stringExample;
    // public bool[] boolArrayExample;

    public SaveData()
    {
        softCoins = 0;
        hardCoins = 0;
        experience = 0;
        energy = 20;
        lastConnection = System.DateTime.Now.ToString();
        talentsList = new int[12];
        inventory = "";
        equipment = "";
        equippedHero = "BasicCar";
        heroMultipliers = new float[20]; // Tamaño = Número de héroes en el juego

        currentWorld = 0;
        highestStageReached = new int[12]; // Tamaño = Número de mundos

        currentDailyLoginReward = 0;

        achievements = "";
        for (int i = 1; i <= 5; ++i)
        {
            if (i < 10)
            {
                achievements += "a0" + i + "-0;";
            }
            else
            {
                achievements += "a" + i + "-0;";
            }
        }
        
        completedDailyQuests = new bool[12];
        reclaimedDailyQuests = new bool[12];
    }
}
