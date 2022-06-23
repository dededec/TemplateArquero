using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardButtonManager : MonoBehaviour
{

    #region FIELDS

    [SerializeField] private EconomyManager.CoinType _paymentMethod = EconomyManager.CoinType.NONE;
    [SerializeField] private int price = 0;
    [SerializeField] private List<Reward> listOfRewards = new List<Reward>();
    [SerializeField] private RewardManager _rewardManager;

    private enum ChestType
    {
        GOLD,
        OBSIDIAN,
        NONE,
    }

    [SerializeField] private ChestType _chestType;

    #endregion

    #region LIFECYCLE

    private void Start() 
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    #endregion
    

    public void ButtonPressed()
    {
        bool userCanPay = true;

        if(_paymentMethod != EconomyManager.CoinType.NONE) userCanPay = EconomyManager.Pay(_paymentMethod, price);

        if(userCanPay)
        {
            switch(_chestType)
            {
                case ChestType.GOLD:
                GameObject.FindGameObjectWithTag("DailyManager").GetComponent<DailyQuestsManager>().ProgressQuest("GoldChestOpen");
                break;
                case ChestType.OBSIDIAN:
                GameObject.FindGameObjectWithTag("DailyManager").GetComponent<DailyQuestsManager>().ProgressQuest("ObsidianChestOpen");
                break;
            }
            
            _rewardManager.GiveReward(listOfRewards);
        }
    }

}
