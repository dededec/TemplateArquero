using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TemplateArquero;
using System;

public class SCP_ButtonManager : MonoBehaviour
{
    [SerializeField] private PurchaseProduct _purchaseProduct;

    private void Start() {
        // SoftCoinManager.Instance.Add(100);
        // HardCoinManager.Instance.Add(100);
        // EnergyManager.Instance.Add(100);
    }

    public void onBuy()
    {
        switch(_purchaseProduct.productType)
        {
            case PurchaseProduct.ProductType.ENERGY:
                if(SoftCoinManager.Instance.Pay(Convert.ToInt32(_purchaseProduct.price)))
                {
                    EnergyManager.Instance.Add(Convert.ToInt32(_purchaseProduct.quantity));
                }
                break;
        }
    }
}
