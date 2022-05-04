using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EconomyManager
{
    // #region Singleton

    // private static EconomyManager _instance;

    // public static EconomyManager Instance
    // {
    //     get
    //     {
    //         return _instance;
    //     }
    // }

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this);

    //     if (_instance == null)
    //     {
    //         _instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    // #endregion

    public enum CoinType
    {
        HARDCOIN,
        SOFTCOIN,
        ENERGY,
    }

    #region Properties

    public static int HardCoins
    {
        get
        {
            return SaveDataController.HardCoins;
        }

        set
        {
            SaveDataController.HardCoins = value;
        }
    }

    public static int SoftCoins
    {
        get
        {
            return SaveDataController.SoftCoins;
        }

        set
        {
            SaveDataController.SoftCoins = value;
        }
    }

    public static int Energy
    {
        get
        {
            return SaveDataController.Energy;
        }

        set
        {
            SaveDataController.Energy = value;
        }
    }

    #endregion

    public static bool Pay(CoinType type, int amount)
    {
        bool result = false;
        switch (type)
        {
            case CoinType.HARDCOIN:
                result = PayInternal(HardCoins, amount);
                break;
            case CoinType.SOFTCOIN:
                result = PayInternal(SoftCoins, amount);
                break;
            case CoinType.ENERGY:
                result = PayInternal(Energy, amount);
                break;
        }

        return result;
    }

    public static void Add(CoinType type, int amount)
    {
        switch (type)
        {
            case CoinType.HARDCOIN:
                AddInternal(HardCoins, amount);
                break;
            case CoinType.SOFTCOIN:
                AddInternal(SoftCoins, amount);
                break;
            case CoinType.ENERGY:
                AddInternal(Energy, amount);
                break;
        }
    }

    private static bool PayInternal(int currency, int amount)
    {
        if (amount > currency)
        {
            return false;
        }

        currency -= amount;
        return true;
    }

    private static void AddInternal(int currency, int amount)
    {
        currency += amount;
    }

}
