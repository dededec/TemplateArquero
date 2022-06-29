using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestAdOpen : TimedObject
{
    public const float HardCoinRow = 0f;

    [Header("Dependencies")]
    [SerializeField] private RewardedAdManager _rewardedAdManager;
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _uiChestOpened;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _uiChestOpenedCloseButton;
    [SerializeField] private ShopScroller _scroller;

    [Header("Chest settings")]
    [SerializeField] private ChestManager.ChestRarity rarity;
    [SerializeField] private int _cost;
    private int _consecutiveBuy;
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
        _rewardedAdManager.ShowAd(new Reward(ChestManager.instance.GenerateChest(rarity).id, 1));
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnButtonClickPay(); });
    }

    public void OnButtonClickPay()
    {
        if (EconomyManager.Pay(EconomyManager.CoinType.HARDCOIN, _cost))
        {
            Item item = ChestManager.instance.GenerateChest(rarity);
            _rewardManager.GiveReward(new Reward(item.id, 1));
            if (_consecutiveBuy < _timesCostReduced)
            {
                _consecutiveBuy++;
                _cost -= _costReduction;
            }

            // 1. Meter UI de recompensa obtenida.
            // 2. Meter UI de pagar de nuevo.
            SetUIChestOpened(true, item);
        }
        else
        {
            // Llevar a que paguen para tener mas hardcoin.
            SetUIChestOpened(false);
            _scroller.FocusOn(HardCoinRow);
        }
    }

    public void ExitConsecutiveBuy()
    {
        _cost += _costReduction * _consecutiveBuy;
        _consecutiveBuy = 0;
        SetUIChestOpened(false);
    }

    private void SetUIChestOpened(bool value, Item item = null)
    {
        if(value)
        {
            _uiChestOpenedCloseButton.onClick.RemoveAllListeners();
            _uiChestOpenedCloseButton.onClick.AddListener(delegate {ExitConsecutiveBuy();});
            _itemIcon.sprite = item.icon;
        }
        
        _uiChestOpened.SetActive(value);
        
    }
}
