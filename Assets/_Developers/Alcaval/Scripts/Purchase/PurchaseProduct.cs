using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Product")]
public class PurchaseProduct : ScriptableObject
{
    public enum Type
    {
        HARDCOIN,
        SOFTCOIN,
        ENERGY,
    }

    public Type type;
    public string quantity;
    public string description;
    public double price;
    public Sprite icon;
}
