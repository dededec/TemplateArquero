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

    // Progress
    public int currentWorld;
    public int[] highestStageReached;

    // Daily Login Reward
    public int currentDailyLoginReward;

    //public bool[] isAchievementCompleted;
    public string achievements;
    public string dailyQuests;

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

        currentWorld = 0;
        highestStageReached = new int[12];

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

        dailyQuests = "";
        for (int i = 1; i <= 20; ++i)
        {
            if (i < 10)
            {
                dailyQuests += "q0" + i + "-0;";
            }
            else
            {
                dailyQuests += "q" + i + "-0;";
            }
        }
    }
}