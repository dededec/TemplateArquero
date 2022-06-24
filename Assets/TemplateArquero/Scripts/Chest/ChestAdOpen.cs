using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestAdOpen : TimedObject
{
    [Header("Dependencies")]
    [SerializeField] private RewardedAdManager _rewardedAdManager;
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _uiChestOpened;

    [Header("Chest settings")]
    [SerializeField] private ChestManager.ChestRarity rarity;
    [SerializeField] private int _cost;
    [SerializeField] private int _consecutiveBuy;
    [SerializeField] private int _costReduction;
    [SerializeField] private int _timesCostReduced;

    protected override void Initialize()
    {
        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalSeconds >= _totalSeconds)
        {
            OnIntervalCompleted();
        }
    }

    protected override void OnIntervalCompleted()
    {
        // Activamos el botón de ver anuncio para abrir cofre.
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnButtonClickAd(); });
    }

    public void OnButtonClickAd()
    {
        _rewardedAdManager.ShowAd(ChestManager.instance.GenerateChest(rarity));
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnButtonClickPay(); });
    }

    public void OnButtonClickPay()
    {
        if (EconomyManager.Pay(EconomyManager.CoinType.HARDCOIN, _cost))
        {
            _rewardManager.GiveReward(ChestManager.instance.GenerateChest(rarity));
            _consecutiveBuy++;
            if (_consecutiveBuy < _timesCostReduced)
            {
                _cost -= _costReduction;
            }

            // 1. Meter UI de recompensa obtenida.
            // 2. Meter UI de pagar de nuevo.
            SetUIChestOpened(true);
        }
        else
        {
            // Llevar a que paguen para tener mas hardcoin.
        }
    }

    public void ExitConsecutiveBuy()
    {
        _cost += _costReduction * _consecutiveBuy;
        _consecutiveBuy = 0;

        SetUIChestOpened(false);
    }

    private void SetUIChestOpened(bool value)
    {
        if(value)
        {
            /*
            -   Poner animación de cofre abriendose y tal.
            -   Poner botones? Verdaderamente no, se ponen desde el editor.
            */
        }
        
        _uiChestOpened.SetActive(value);
        
    }
}
