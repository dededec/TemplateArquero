using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{

    private string example = "com.CompanyName.NameOfGame.example";

    public void OnPurchaseComplete(Product product)
    {
        //The player has made the example purchase
        if(product.definition.id == example) 
        {
            Debug.Log("Made a purchase of example");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " FALLIDO ");
    }
}
