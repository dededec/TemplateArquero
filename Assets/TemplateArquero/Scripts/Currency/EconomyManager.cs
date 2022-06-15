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
        NONE,
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
                HardCoins = PayInternal(HardCoins, amount);
                result = HardCoins >= amount;
                break;
            case CoinType.SOFTCOIN:
                SoftCoins = PayInternal(SoftCoins, amount);
                result = SoftCoins >= amount;
                break;
            case CoinType.ENERGY:
                Energy = PayInternal(Energy, amount);
                result = Energy >= amount;
                break;
        }
        onValuesChanged();
        return result;
    }

    public static void Add(CoinType type, int amount)
    {
        switch (type)
        {
            case CoinType.HARDCOIN:
                HardCoins = AddInternal(HardCoins, amount);
                break;
            case CoinType.SOFTCOIN:
                SoftCoins = AddInternal(SoftCoins, amount);
                break;
            case CoinType.ENERGY:
                Energy = AddInternal(Energy, amount);
                break;
        }
        onValuesChanged();
    }

    private static int PayInternal(int currency, int amount)
    {
        if (amount > currency)
        {
            return currency;
        }

        currency -= amount;
        return currency;
    }

    private static int AddInternal(int currency, int amount)
    {
        currency += amount;
        return currency;
    }

    public delegate void OnValuesChanged();
    public static event OnValuesChanged onValuesChanged;

}
