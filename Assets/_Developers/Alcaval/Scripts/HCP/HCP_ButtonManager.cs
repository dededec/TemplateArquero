using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TemplateArquero;
using System;

public class HCP_ButtonManager : MonoBehaviour
{
    [SerializeField] private int _adUses;
    [SerializeField] private bool _canUseAds;
    [SerializeField] private GameObject _adButton;
    [SerializeField] private GameObject _priceText;
    [SerializeField] private PurchaseProduct _purchaseProduct;

    private void Start() {
        if(_canUseAds) 
        {
            _adButton.gameObject.SetActive(true); 
            _priceText.gameObject.SetActive(false);    
        }
        else 
        { 
            _adButton.gameObject.SetActive(false); 
            _priceText.gameObject.SetActive(true);
        }
    }

    private void Update() {
        if(_adUses > 0)
        {
            _adButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            _adButton.GetComponent<Button>().interactable = false;
        }
    }

    public void minusUse()
    {
        _adUses--;
    }

    public void onBuy()
    {
        if(!_canUseAds)
        {
            switch(_purchaseProduct.type)
            {
                case PurchaseProduct.Type.SOFTCOIN:
                    //HardCoinManager.Instance.Pay(_purchaseProduct.price);
                    SoftCoinManager.Instance.Add(100);
                    Debug.Log("Has comprado " + _purchaseProduct.quantity + " " + _purchaseProduct.type);
                    break;
                case PurchaseProduct.Type.ENERGY:
                    EnergyManager.Instance.Add(100);
                    Debug.Log("Has comprado " + _purchaseProduct.quantity + " " + _purchaseProduct.type);
                    break;
            }
        }
    }

}
