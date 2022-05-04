using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// TemplateArquero;
using System;

public class EP_ButtonManager : MonoBehaviour
{
    [SerializeField] private PurchaseProduct _purchaseProduct;

    public void onBuy()
    {
        // switch(_purchaseProduct.productType)
        // {
        //     case PurchaseProduct.ProductType.GAME:
        //         if(EnergyManager.Instance.Substract(Convert.ToInt32(_purchaseProduct.price)))
        //         {
        //             Debug.Log("Partida comenzada");
        //         }
        //         break;
        // }
    }
}
