using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCoinManager : EconomyManager
{
    #region Singleton

    private static SoftCoinManager _instance;

    public static SoftCoinManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Properties

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

    #endregion

    #region Public Methods

    public override bool Pay(int amount)
    {
        if(amount > SoftCoins)
        {
            return false;
        }

        Debug.Log("Se han pagado soft coins.");
        SoftCoins -= amount;
        return true;
    }

    public override void Add(int amount)
    {
        Debug.Log("Se han añadido soft coins.");
        SoftCoins += amount;

        EconomyManager a = this.GetComponent<SoftCoinManager>();
        a.Pay(2);
    }

    

    #endregion
}