using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCoinManager : MonoBehaviour
{
    #region Fields

    private static SoftCoinManager _instance;
    private int _currentValue;

    #endregion

    #region Properties

    public static SoftCoinManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public int Value
    {
        get
        {
            return _currentValue;
        }
    }

    #endregion

    #region Life Cycle

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

    #region Public Methods

    public bool Pay(int quantity)
    {
        if(quantity > _currentValue)
        {
            return false;
        }

        Debug.Log("Se han añadido soft coin");
        _currentValue -= quantity;
        return true;
    }

    public void Add(int quantity) => Pay(-quantity);

    #endregion
}