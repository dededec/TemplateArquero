using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TemplateArquero;
using System;

public class HCP_ButtonManager : MonoBehaviour
{
    //In case the purchase has an ad option
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
            switch(_purchaseProduct.productType)
            {
                case PurchaseProduct.ProductType.SOFTCOIN:
                    if(HardCoinManager.Instance.Pay(Convert.ToInt32(_purchaseProduct.price)))
                    {
                        SoftCoinManager.Instance.Add(Convert.ToInt32(_purchaseProduct.quantity));
                    }
                    break;
                case PurchaseProduct.ProductType.ENERGY:
                    if(HardCoinManager.Instance.Pay(Convert.ToInt32(_purchaseProduct.price)))
                    {
                        EnergyManager.Instance.Add(Convert.ToInt32(_purchaseProduct.quantity));
                    }
                    break;
            }
        }
    }

}
