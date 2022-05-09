using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum TypeOfReward
    {
        HARDCOIN,
        SOFTCOIN,
        ENERGY,
        EQUIPMENT,
        
    }

    public enum InventoryUse
    {
        PAYMENT,
        SLOT1,
        SLOT2,
        SLOT3,
        SLOT4,
        SLOT5,
        SLOT6,
        UPGRADE_WEAPON,
    }

    public enum Rarity
    {
        RARE,
        COMMON,
        COIN,
    }

    public string id;
    public TypeOfReward typeOfReward;
    public InventoryUse inventoryUse;
    public Rarity rarity;
    public new string name;
    public string description;
    public Sprite icon;
}
