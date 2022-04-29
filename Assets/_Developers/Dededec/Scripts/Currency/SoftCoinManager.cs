using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCoinManager : MonoBehaviour
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

    public bool Pay(int quantity)
    {
        if(quantity > SoftCoins)
        {
            return false;
        }

        Debug.Log("Se han pagado soft coins.");
        SoftCoins -= quantity;
        return true;
    }

    public void Add(int quantity)
    {
        Debug.Log("Se han añadido soft coins.");
        SoftCoins += quantity;
    }

    #endregion
}