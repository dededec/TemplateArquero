using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int softCoins;
    public bool boolExample;
    public int intExample;
    public string stringExample;
    public bool[] boolArrayExample;

    public SaveData()
    {
        boolArrayExample = new bool[6];

    }
}
