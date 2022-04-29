using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Product")]
public class PurchaseProduct : ScriptableObject
{
    public enum PaymentMethod
    {
        HARDCOIN,
        SOFTCOIN,
        ENERGY,
        MONEY,
    }

    public enum ProductType
    {
        HARDCOIN,
        SOFTCOIN,
        ENERGY,
        GAME,
        CHEST,
    }

    public PaymentMethod paymentMethod;
    public ProductType productType;
    public string quantity;
    public string description;
    public double price;
    public Sprite icon;
}
