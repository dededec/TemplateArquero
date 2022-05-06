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

    #endregion

    #region LIFECYCLE

    #endregion
    

    public void ButtonPressed()
    {
        bool userCanPay = true;

        if(_paymentMethod != EconomyManager.CoinType.NONE) userCanPay = EconomyManager.Pay(_paymentMethod, price);

        if(userCanPay)
        {
            _rewardManager.GiveReward(listOfRewards);
        }
    }

}