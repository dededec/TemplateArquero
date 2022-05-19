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
        ACCESORIES,
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
    public int level;
    public float multiplier;
    public Sprite icon;

    public void init(string id, string name, TypeOfReward typeOfReward, InventoryUse inventoryUse, Rarity rarity, string description, string icon)
    {
        this.id = id;
        this.typeOfReward = typeOfReward;
        this.inventoryUse = inventoryUse;
        this.rarity = rarity;
        this.name = name;
        this.description = description;
        // this.level = level;
        // this.multiplier = multiplier;
        // TODO read item icon
    }

}
