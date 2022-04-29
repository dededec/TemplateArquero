using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int softCoins;
    public int hardCoins;
    public int energy;

    // Last Time Connected
    // day, month, year, hour, minute, second
    public string lastConnection;

    public bool boolExample;
    public int intExample;
    public string stringExample;
    public bool[] boolArrayExample;

    public SaveData()
    {
        softCoins = 0;
        hardCoins = 0;
        lastConnection = System.DateTime.Now.ToString();

        boolArrayExample = new bool[6];
    }
}
