using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private List<IAPReward> _IAPRewardList;
    
    [SerializeField] private RewardManager _rewardManager;

    [System.Serializable]
    public class IAPReward
    {
        public string idProduct;
        public List<Reward> rewards;
    }

    public void OnPurchaseComplete(Product product)
    {
        foreach(IAPReward IAPr in _IAPRewardList)
        {
            if(IAPr.idProduct == product.definition.id)
            {
                _rewardManager.GiveReward(IAPr.rewards);
            }
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " FALLIDO ");
    }
}
