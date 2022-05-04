using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButtonController : MonoBehaviour
{
    [SerializeField] public RewardedAdManager _rewardedAdManager;    
    [SerializeField] private List<Reward> _listOfRewards;
    
    private void Start() 
    {
        transform.GetComponent<Button>().onClick.AddListener(delegate{ _rewardedAdManager.ShowAd(_listOfRewards); }) ;
    }

    void OnDestroy()
    {
        transform.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
