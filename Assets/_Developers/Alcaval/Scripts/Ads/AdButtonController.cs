using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButtonController : MonoBehaviour
{
    [SerializeField] public RewardedAdManager _rewardedAdManager;    
    [SerializeField] RewardedAdManager.Reward reward;
    
    private void Start() 
    {
        transform.GetComponent<Button>().onClick.AddListener(delegate{ _rewardedAdManager.ShowAd(reward); }) ;
    }

    void OnDestroy()
    {
        // Clean up the button listeners:
        transform.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
